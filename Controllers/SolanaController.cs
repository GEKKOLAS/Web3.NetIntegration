using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MordexIntegration.Services;


namespace MordexIntegration.Controllers
{
    public class SolanaController : Controller
    {
        private readonly SolanaServices _solanaService;
        public SolanaController()
        {
            _solanaService = new SolanaServices();
        }

        [HttpGet]
        [Route("Solana")]
        public IActionResult Solana()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAccount()
        {
            var (account, privateKey, mnemonic) = _solanaService.CreateAccount();
            HttpContext.Session.SetString("PublicKey", account.PublicKey.Key);
            HttpContext.Session.SetString("PrivateKey", privateKey);
            HttpContext.Session.SetString("Mnemonic", mnemonic);
            ViewBag.PublicKey = account.PublicKey.Key;
            ViewBag.PrivateKey = privateKey;
            ViewBag.Mnemonic = mnemonic;
            return View("Solana");
        }

        [HttpPost]
        public async Task<IActionResult> CheckBalance(string publicKey)
        {
            try
            {
                var balance = await _solanaService.GetBalanceAsync(publicKey);
                ViewBag.Balance = balance;
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View("Solana");
        }

        [HttpPost]
        public async Task<IActionResult> RequestAirdrop(string publicKey)
        {
            try
            {
                await _solanaService.RequestAirdropAsync(publicKey, 1000000000); // 1 SOL
                ViewBag.AirdropSuccess = true;
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View("Solana");
        }

        [HttpPost]
        public async Task<IActionResult> GetTransactionHistory(string publicKey)
        {
            try
            {
                var transactions = await _solanaService.GetTransactionHistoryAsync(publicKey);
                ViewBag.Transactions = transactions;
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View("Solana");
        }

        [HttpPost]
        public IActionResult ClearSession()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Solana");
        }
    }
}
