using Event_Management_System.Models;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace Event_management_Api.Etc;

public class Odata
{
    public static IEdmModel GetEdmModel()
    {
        var edmBuilder = new ODataConventionModelBuilder();
        edmBuilder.EntitySet<Event>("ODataEvents");
        return edmBuilder.GetEdmModel();
    }
}
