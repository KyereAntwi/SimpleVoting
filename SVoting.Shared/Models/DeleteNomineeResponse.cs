namespace SVoting.Shared.Models
{
    public class DeleteNomineeResponse : BaseResponse
    {
        public DeleteNomineeResponse() : base() { }

        public Guid NomineeId { get; set; }    
    }
}
