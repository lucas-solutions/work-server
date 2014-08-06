
namespace Lucas.Solutions.Diagnostics
{
    using Lucas.Solutions.Diagnostics.Responses;

    public interface IFacet
    {
        FacetResponse GetDate(string query);
        FacetResponse GetDate(FacetQuery query);
        FacetResponse GetIp(string query);
        FacetResponse GetIp(FacetQuery query);
        FacetResponse GetInput(string query);
        FacetResponse GetInput(FacetQuery query);
    }
}