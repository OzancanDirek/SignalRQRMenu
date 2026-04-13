using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Drawing;

namespace SignalRWebUI.Controllers
{
    public class QRCodeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
          return View();
        }

        [HttpPost]
        public IActionResult Index(string value)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(value, QRCodeGenerator.ECCLevel.Q);

            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeImage = qrCode.GetGraphic(10);

            ViewBag.QRCodeImage = "data:image/png;base64," + Convert.ToBase64String(qrCodeImage);

            return View();
        }
    }
}