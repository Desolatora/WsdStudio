using System;

namespace WsdPreprocessingStudio.Core.Resources
{
    internal static class ExceptionMessage
    {
        public const string DestinationPathMustBeEmptyAndExisting =
            "Destination path must be an empty existing directory.";

        public const string PathMustBeExistingWsdProj =
            "Path must be an existing .wsdproj file.";

        public const string UnableToLoadProjectData =
            "Unable to load project data. Project may be corrupt.";

        public static readonly Func<string, string, string> ProjectVersionNotSupported =
            (current, supported) => $"Project version '{current}' is not supported. " +
                                    $"Supported version is '{supported}'.";
    }
}
