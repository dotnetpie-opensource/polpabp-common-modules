using System;

namespace PolpAbp.ResourceManagement
{
    public static class ResourceManagementTestConsts
    {
        public static Guid TenantId = new Guid("34977DB9-37F2-4044-9F80-3540694F0D77");
        public static Guid AdminId = new Guid("3D7321A4-7C67-4465-B5E6-37CB7254EEA2");
        public static Guid OrgUnitId = new Guid("E34391B1-B62A-4AF2-9C06-EFC8AC8FB41A");
        public static string AdminEmail = "test@polpabp.com";
        public static string AdminPass = "1q2w3eA!";

        public static Guid MemberUserId1 = new Guid("A8CAFF1C-F289-4300-B38D-2E88F68E7449");

        public static string UserName1 = "User1";
        public static string UserName2 = "User2";
        public static string User1Email = "user1@gmail.com";
        public static string User2Email = "user2@gmail.com";

        // Plan Id
        public static Guid FirstPlanId = new Guid("996fc873-a501-4581-aa39-1c63b14f7fb0");
        public static Guid SecondPlanId = new Guid("14b71456-71fa-4919-a43a-684854b19683");

        // Group 
        public static string Group1DsiplayName = "Group1";

        // resource 
        public static string SmsResourceName = nameof(SmsResourceName);
    }
}
