using AesEncription.DTO;
using AesEncription.Helper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace AesEncription.Controllers
{
    [Route("api/v1/encryption")]
    [ApiController]
    public class EncryptionController : ControllerBase
    {
        private readonly AesEncriptionHelper _aesHelper;

        public EncryptionController()
        {
            string key = "1234567890123456"; 
            string iv = "6543210987654321";
            _aesHelper = new AesEncriptionHelper(key, iv);
        }
        

        [HttpPost("Encrypt")]
        public IActionResult Encrypt(IFormFile file)
        {
            try
            {
                string inputPath = Path.Combine(Path.GetTempPath(), file.FileName);
                string outputPath = Path.Combine(Path.GetTempPath(), "encrypted_" + file.FileName);

                using (var stream = new FileStream(inputPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                _aesHelper.Encrypt(inputPath, outputPath);
                var encryptedBytes = System.IO.File.ReadAllBytes(outputPath);
                return File(encryptedBytes, "application/octet-stream", "encrypted_" + file.FileName);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }




        [HttpPost("Decrypt")]
        public IActionResult Decrypt(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            try
            {
                string inputPath = Path.Combine(Path.GetTempPath(), file.FileName);
                string outputPath = Path.Combine(Path.GetTempPath(), "decrypted_" + file.FileName);

                using (var stream = new FileStream(inputPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                _aesHelper.Decrypt(inputPath, outputPath);
                var decryptedFile = System.IO.File.ReadAllBytes(outputPath);
                return File(decryptedFile, "application/octet-stream", "decrypted_" + file.FileName);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
