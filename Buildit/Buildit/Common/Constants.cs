using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Buildit.Common.Providers
{
    public static class Constants
    {
        public const int PublicationsPerPage = 9;
        public const string PublicationTypeCache = "publicationtypes";
        public const int PublicationTypeExpirationInMinutes = 20;
        public const string AdminRole = "Admin";
        public const string RegularRole = "Regular";
        public const string ViewModelsAssembly = "CourseProject.ViewModels";

        public const string PictureErrorMessage = "The chosen picture should be an image file.";
        public const string TitleExistsErrorMessage = "There is already a publication with the same name";
        public const string ImagesRelativePath = "~/Content/Images/";
        public const string AddPublicationSuccessMessage = "Your publication was added successfully.";
        public const string AddPublicationSuccessKey = "Addition";

        public const int TopPublicationsCount = 8;
        public const string TopPublicationsCache = "topPublications";
        public const int TopPublicationsExpirationInMinutes = 10;

        public const int MinRating = 1;
        public const int MaxRating = 5;
        public const string RatingErrorMessage = "Rating not valid";
    }
}