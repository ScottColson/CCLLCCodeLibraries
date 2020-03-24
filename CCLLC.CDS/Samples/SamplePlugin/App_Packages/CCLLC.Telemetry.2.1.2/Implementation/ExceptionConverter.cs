﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using CCLLC.Telemetry.DataContract;

namespace CCLLC.Telemetry.Implementation
{
    internal static class ExceptionConverter
    {
        public const int MaxParsedStackLength = 32768;

        
        internal static IExceptionDetails ConvertToExceptionDetails(Exception exception, IExceptionDetails parentExceptionDetails)
        {
            IExceptionDetails exceptionDetails = ExceptionDetails.CreateWithoutStackInfo(exception, parentExceptionDetails);
            var stack = new StackTrace(exception, true);

            var frames = stack.GetFrames();
            Tuple<List<IStackFrame>, bool> sanitizedTuple = SanitizeStackFrame(frames, GetStackFrame, GetStackFrameLength);
            exceptionDetails.parsedStack = sanitizedTuple.Item1;
            exceptionDetails.hasFullStack = sanitizedTuple.Item2;
            return exceptionDetails;
        }

        /// <summary>
        /// Converts a System.Diagnostics.StackFrame to a Microsoft.ApplicationInsights.Extensibility.Implementation.TelemetryTypes.StackFrame.
        /// </summary>
        internal static IStackFrame GetStackFrame(System.Diagnostics.StackFrame stackFrame, int frameId)
        {
            var convertedStackFrame = new Telemetry.DataContract.StackFrame()
            {
                level = frameId
            };

            var methodInfo = stackFrame.GetMethod();
            string fullName;
            if (methodInfo.DeclaringType != null)
            {
                fullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
            }
            else
            {
                fullName = methodInfo.Name;
            }

            convertedStackFrame.method = fullName;
            convertedStackFrame.assembly = methodInfo.Module.Assembly.FullName;
            convertedStackFrame.fileName = stackFrame.GetFileName();

            // 0 means it is unavailable
            int line = stackFrame.GetFileLineNumber();
            if (line != 0)
            {
                convertedStackFrame.line = line;
            }

            return convertedStackFrame;
        }

        /// <summary>
        /// Gets the stack frame length for only the strings in the stack frame.
        /// </summary>
        internal static int GetStackFrameLength(IStackFrame stackFrame)
        {
            var stackFrameLength = (stackFrame.method == null ? 0 : stackFrame.method.Length)
                                   + (stackFrame.assembly == null ? 0 : stackFrame.assembly.Length)
                                   + (stackFrame.fileName == null ? 0 : stackFrame.fileName.Length);
            return stackFrameLength;
        }

        /// <summary>
        /// Sanitizing stack to 32k while selecting the initial and end stack trace.
        /// </summary>
        private static Tuple<List<TOutput>, bool> SanitizeStackFrame<TInput, TOutput>(
            IList<TInput> inputList,
            Func<TInput, int, TOutput> converter,
            Func<TOutput, int> lengthGetter)
        {
            List<TOutput> orderedStackTrace = new List<TOutput>();
            bool hasFullStack = true;
            if (inputList != null && inputList.Count > 0)
            {
                int currentParsedStackLength = 0;
                for (int level = 0; level < inputList.Count; level++)
                {
                    // Skip middle part of the stack
                    int current = (level % 2 == 0) ? (inputList.Count - 1 - (level / 2)) : (level / 2);

                    TOutput convertedStackFrame = converter(inputList[current], current);
                    currentParsedStackLength += lengthGetter(convertedStackFrame);

                    if (currentParsedStackLength > ExceptionConverter.MaxParsedStackLength)
                    {
                        hasFullStack = false;
                        break;
                    }

                    orderedStackTrace.Insert(orderedStackTrace.Count / 2, convertedStackFrame);
                }
            }

            return new Tuple<List<TOutput>, bool>(orderedStackTrace, hasFullStack);
        }
    }
}