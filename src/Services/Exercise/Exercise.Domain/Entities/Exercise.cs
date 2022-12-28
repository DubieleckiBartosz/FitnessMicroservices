using Exercise.Domain.Base;
using Exercise.Domain.ValueObjects;

namespace Exercise.Domain.Entities;

public class Exercise : Entity
{
    public string Name { get; }
    public string CreatedBy { get; }
    public Link? Video { get; private set; }
    public ExerciseDescription Description { get; private set; }
    public List<ExerciseImage> Images { get; private set; }

    private Exercise(string name, string createdBy, ExerciseDescription description)
    {
        Id = Guid.NewGuid();
        Name = name;
        CreatedBy = createdBy;
        Description = description;
        Images = new List<ExerciseImage>();
    }

    private Exercise(Guid id, string name, string createdBy, ExerciseDescription description, List<ExerciseImage>? images)
    {
        Id = id;
        Name = name;
        CreatedBy = createdBy;
        Description = description;
        Images = images ?? new List<ExerciseImage>();
    }

    public static Exercise Create(string name, string createdBy, ExerciseDescription description)
    {
        return new Exercise(name, createdBy, description);
    }

    public static Exercise Load(Guid id, string name, string createdBy, ExerciseDescription description, List<ExerciseImage> images)
    {
        return new Exercise(id, name, createdBy, description, images);
    }

    public static Exercise Load(Guid id, string name, string createdBy, ExerciseDescription description)
    {
        return new Exercise(id, name, createdBy, description, null);
    }

    public void UpdateDescription(ExerciseDescription newDescription)
    {
        Description = newDescription;
    }

    public void AssignVideoLink(Link videoLink)
    {
        Video = videoLink;
    }

    public ExerciseImage AddNewImage(string path, string title, bool isMain, ImageDescription imageDescription)
    { 

        var newImage = ExerciseImage.Create(this.Id, path, title, isMain, imageDescription); 
         
        if (!this.Images.Any() && !isMain)
        {
            throw new ExerciseBusinessException("The image must be the main image when the image list is empty.", "Image must be main"); 
        }

        if (this.Images.Any())
        {
            var result = this.Images.FirstOrDefault(_ => _.ImageTitle == title);
            if (result != null)
            {
                throw new ExerciseBusinessException("Such an image already exists.", "Bad image title");
            }
        }

        if (this.Images.Any() && isMain)
        {
            var currentMainImage = this.Images.First(_ => _.IsMain);
            currentMainImage?.ChangeStatusMain();
        }

        this.Images.Add(newImage);

        return newImage;
    }
}