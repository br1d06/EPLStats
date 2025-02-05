namespace WOD.WebUI.ViewModels;

public class PrivacyViewModel
{
	public string? RequestId { get; set; }

	public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}

