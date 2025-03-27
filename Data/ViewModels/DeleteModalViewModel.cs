namespace VetLife.Data.ViewModels
{
    public class DeleteModalViewModel
    {
        public string ModalId { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string? HiddenInputName { get; set; }
        public object ItemId { get; set; }
        public string ConfirmationMessage { get; set; }
    }
}
