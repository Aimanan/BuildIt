using Buildit.Data.Contracts;
using Buildit.Data.Models;
using Buildit.Services.Contracts;
using Buildit.Web.Models;
using Bytes2you.Validation;
using System.Collections.Generic;
using System.Linq;

namespace Buildit.Services
{
    public class PublicationService : IPublicationService
    {
        private readonly IBuilditData data;

        public PublicationService(IBuilditData data)
        {
            Guard.WhenArgument(data, "Data").IsNull().Throw();

            this.data = data;
        }

        public bool PublicationFound(string title)
        {
            var exists = this.data.Publications.All.Any(x => x.Title == title);
            return exists;
        }

        public int AddPublication(PublicationViewModelAdmin publModel, string fileName)
        {
            var publication = new Publication()
            {
                Title = publModel.Title,
                Author = publModel.Author,
                Description = publModel.Description,
                Content=publModel.Content,
                PublishedOn = publModel.PublishedOn,
                PublicationTypeId = publModel.PublicationTypeId,
                Picture = fileName
            };

            this.data.Publications.Add(publication);
            this.data.SaveChanges();

            return publication.Id;
        }

        public IEnumerable<Publication> GetTopPublications(int count)
        {
            var publications = this.data.Publications.All
                .OrderByDescending(x => x.Ratings.Sum(y => y.Value) / (double)x.Ratings.Count)
                .Take(count)
                .ToList();

            return publications;
        }


        public double GetPublicationRating(int id)
        {
            var publication = this.data.Publications.All
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (publication != null)
            {
                return publication.RatingCalculated;
            }
            else
            {
                return 0;
            }
        }

        public int GetPublicationsCount(string searchWord, IEnumerable<int> publicationTypeIds)
        {
            var publications = this.BuildFilterQuery(searchWord, publicationTypeIds);
            return publications.Count();
        }


        public Publication GetById(int id)
        {
            var publication = this.data.Publications.All
                .Where(x => x.Id == id)
                //.Include(x => x.PublicationType)
                .FirstOrDefault();

            return publication;
        }

        public IEnumerable<Publication> SearchPublications(string searchWord, IEnumerable<int> publicationTypeIds, string orderProperty, int page = 1, int publicationsPerPage = 9)
        {
            var skip = (page - 1) * publicationsPerPage;

            var publications = this.BuildFilterQuery(searchWord, publicationTypeIds);

            orderProperty = orderProperty == null ? string.Empty : orderProperty.ToLower();
            switch (orderProperty)
            {
                case "author":
                    publications = publications.OrderBy(x => x.Author);
                    break;
                case "type":
                    publications = publications.OrderByDescending(x => x.PublicationType);
                    break;
                default:
                    publications = publications.OrderBy(x => x.Title);
                    break;
            }

            var resultpublications = publications
                .Skip(skip)
                .Take(publicationsPerPage)
                .ToList();

            return resultpublications;
        }

        private IQueryable<Publication> BuildFilterQuery(string searchWord, IEnumerable<int> publicationTypeIds)
        {
            var publications = this.data.Publications.All;

            if (searchWord != null)
            {
                publications = publications.Where(x => x.Title.Contains(searchWord) || x.Author.Contains(searchWord));
            }

            if (publicationTypeIds != null && publicationTypeIds.Any())
            {
                publications = publications.Where(x => publicationTypeIds.Contains(x.PublicationTypeId));
            }

            return publications;
        }
    }
}
