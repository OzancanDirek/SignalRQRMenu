using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using SignalRWebUI.Dtos.MailDtos;

namespace SignalRWebUI.Controllers
{
    public class MailController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(CreateMailDto createMailDto)
        {
            if (string.IsNullOrWhiteSpace(createMailDto.ReceiverMail) ||
                !new System.Net.Mail.MailAddress(createMailDto.ReceiverMail).Address
                    .Equals(createMailDto.ReceiverMail, StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("ReceiverMail", "Geçerli bir e-posta adresi giriniz.");
                return View(createMailDto);
            }

            try
            {
                MimeMessage mimeMessage = new MimeMessage();
                MailboxAddress mailboxAddressFrom = new MailboxAddress("O&D Restourant", "ozandirek820@gmail.com");
                mimeMessage.From.Add(mailboxAddressFrom);
                MailboxAddress mailboxAddressTo = new MailboxAddress("User", createMailDto.ReceiverMail);
                mimeMessage.To.Add(mailboxAddressTo);

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = $@"
                    <!DOCTYPE html>
                    <html>
                    <head><meta charset='UTF-8'><meta name='viewport' content='width=device-width, initial-scale=1.0'></head>
                    <body style='margin:0; padding:0; background-color:#f0ede8; font-family: Arial, sans-serif;'>
                      <table width='100%' cellpadding='0' cellspacing='0' style='background:#f0ede8; padding:32px 0;'>
                        <tr><td align='center'>
                          <table width='560' cellpadding='0' cellspacing='0' style='max-width:560px; width:100%;'>

                            <!-- HEADER -->
                            <tr>
                              <td style='background:#1a1a2e; border-radius:12px 12px 0 0; padding:32px 32px 24px; text-align:center;'>
                                <p style='margin:0 0 4px; color:#c8a97e; font-size:20px; font-weight:bold; letter-spacing:1px;'>O&amp;D Restaurant</p>
                                <p style='margin:0; color:#888888; font-size:13px;'>Rezervasyon Onayı</p>
                              </td>
                            </tr>

                            <!-- HERO BAND -->
                            <tr>
                              <td style='background:#c8a97e; padding:14px 32px;'>
                                <p style='margin:0; color:#1a1a2e; font-size:14px; font-weight:bold;'>&#10003; &nbsp;Rezervasyonunuz başarıyla alındı</p>
                              </td>
                            </tr>

                            <!-- BODY -->
                            <tr>
                              <td style='background:#ffffff; padding:32px; border:1px solid #e8e4de; border-top:none;'>

                                <p style='color:#555555; font-size:14px; margin:0 0 24px; line-height:1.6;'>
                                  Merhaba <strong style='color:#1a1a2e;'>{createMailDto.ReceiverMail}</strong>,<br/>
                                  Sizi aramızda görmekten büyük mutluluk duyarız.
                                </p>

                                <!-- KONU -->
                                <p style='font-size:11px; font-weight:bold; color:#c8a97e; letter-spacing:1.5px; text-transform:uppercase; margin:0 0 10px;'>Konu</p>
                                <table width='100%' cellpadding='0' cellspacing='0' style='margin-bottom:20px;'>
                                  <tr>
                                    <td style='background:#f8f5f0; border-radius:8px; padding:14px 16px;'>
                                      <p style='font-size:15px; font-weight:bold; color:#1a1a2e; margin:0;'>{createMailDto.Subject}</p>
                                    </td>
                                  </tr>
                                </table>

                                <!-- MESAJ -->
                                <p style='font-size:11px; font-weight:bold; color:#c8a97e; letter-spacing:1.5px; text-transform:uppercase; margin:0 0 10px;'>Mesaj</p>
                                <table width='100%' cellpadding='0' cellspacing='0' style='margin-bottom:28px;'>
                                  <tr>
                                    <td width='3' style='background:#c8a97e; border-radius:3px;'></td>
                                    <td style='background:#f8f5f0; border-radius:0 8px 8px 0; padding:14px 16px;'>
                                      <p style='font-size:14px; color:#555555; margin:0; line-height:1.6;'>{createMailDto.Body}</p>
                                    </td>
                                  </tr>
                                </table>

                                <!-- CTA -->
                                <table width='100%' cellpadding='0' cellspacing='0' style='margin-bottom:24px;'>
                                  <tr>
                                    <td align='center' style='background:#1a1a2e; border-radius:8px; padding:14px;'>
                                      <a href='https://google.com' style='color:#c8a97e; text-decoration:none; font-size:14px; font-weight:bold; letter-spacing:0.5px;'>Rezervasyonu Görüntüle</a>
                                    </td>
                                  </tr>
                                </table>

                                <p style='font-size:13px; color:#aaaaaa; text-align:center; margin:0;'>
                                  İptal veya değişiklik için <span style='color:#c8a97e;'>rezervasyon@odrestaurant.com</span> adresine yazabilirsiniz.
                                </p>

                              </td>
                            </tr>

                            <!-- FOOTER -->
                            <tr>
                              <td style='background:#f0ede8; border:1px solid #e8e4de; border-top:none; border-radius:0 0 12px 12px; padding:14px 32px; text-align:center;'>
                                <p style='font-size:12px; color:#aaaaaa; margin:0;'>Bu e-posta otomatik olarak gönderilmiştir &bull; O&amp;D Restaurant &copy; 2025</p>
                              </td>
                            </tr>

                          </table>
                        </td></tr>
                      </table>
                    </body>
                    </html>"; 
                mimeMessage.Body = bodyBuilder.ToMessageBody();
                mimeMessage.Subject = createMailDto.Subject;

                SmtpClient client = new SmtpClient();
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("ozandirek820@gmail.com", "vfml qqfo ngxq eoxg");
                client.Send(mimeMessage);
                client.Disconnect(true);
            }
            catch (FormatException)
            {
                ModelState.AddModelError("ReceiverMail", "Geçerli bir e-posta adresi giriniz.");
                return View(createMailDto);
            }
            catch (SmtpCommandException ex)
            {
                ModelState.AddModelError("", $"Mail gönderilemedi: {ex.Message}");
                return View(createMailDto);
            }

            return RedirectToAction("Index", "Category");
        }
    }
}
