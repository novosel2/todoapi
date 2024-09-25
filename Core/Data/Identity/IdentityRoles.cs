using Microsoft.AspNetCore.Identity;

namespace Core.Data.Identity
{
    public static class IdentityRoles
    {
        private static List<IdentityRole> roles = new List<IdentityRole>();


        public static List<IdentityRole> GetRoles()
        {
            AddRole("admin");
            AddRole("user");

            return roles;
        }

        private static void AddRole(string roleName)
        {
            roles.Add(new IdentityRole()
            {
                Name = roleName,
                NormalizedName = roleName.ToUpper()
            });
        }
    }
}
