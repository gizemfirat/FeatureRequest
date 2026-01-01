namespace FeatureRequestProject.Permissions;

public static class FeatureRequestProjectPermissions
{
    public const string GroupName = "FeatureRequestProject";

    public static class FeatureRequests
    {
        public const string Default = GroupName + ".FeatureRequests";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
        public const string Create = Default + ".Create";
    }
}
