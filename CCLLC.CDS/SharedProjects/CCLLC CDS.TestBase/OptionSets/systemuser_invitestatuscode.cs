namespace TestProxy
{

	
	[System.Runtime.Serialization.DataContractAttribute()]
	[System.CodeDom.Compiler.GeneratedCodeAttribute("CrmSvcUtil", "9.1.0.41")]
	public enum systemuser_invitestatuscode
	{
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		InvitationNotSent = 0,
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		Invited = 1,
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		InvitationNearExpired = 2,
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		InvitationExpired = 3,
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		InvitationAccepted = 4,
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		InvitationRejected = 5,
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		InvitationRevoked = 6,
	}
}
