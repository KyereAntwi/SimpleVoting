namespace SVoting.Shared.Models
{
    public class CreateANomineeResponse : BaseResponse
    {
        public CreateANomineeResponse() : base()
        {
                
        }

        public NomineeDto? NomineeDto { get; set; }
    }
}
