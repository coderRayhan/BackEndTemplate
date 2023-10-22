﻿namespace Domain.Constants;
public static class Permissions
{
    public static List<string> GeneratePermissionsForModule(string module)
    {
        return new List<string>
        {
            $"Permissions.{module}.Create",
            $"Permissions.{module}.View",
            $"Permissions.{module}.Edit",
            $"Permissions.{module}.Delete"
        };
    }

    public static class ApplicationUsers
    {
        public const string Create = "Permissions.ApplicationUsers.Create";
        public const string View = "Permissions.ApplicationUsers.View";
        public const string Edit = "Permissions.ApplicationUsers.Edit";
        public const string Delete = "Permissions.ApplicationUsers.Delete";
    }
}
