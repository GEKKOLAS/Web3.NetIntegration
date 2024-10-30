using Solnet.Rpc;
using Solnet.Rpc.Models;
using Solnet.Wallet;
using Solnet.Wallet.Bip39;
namespace MordexIntegration.Services
{
    public class SolanaServices
    {
        private readonly IRpcClient _rpcClient;

        public SolanaServices()
        {
            _rpcClient = ClientFactory.GetClient(Cluster.DevNet);
        }
        public (Account, string, string) CreateAccount()
        {
            var mnemonic = new Mnemonic(WordList.English, WordCount.Twelve);
            var wallet = new Wallet(mnemonic);
            var account = wallet.Account;
            return (account, Convert.ToBase64String(account.PrivateKey.KeyBytes), mnemonic.ToString());
        }

        public async Task<ulong> GetBalanceAsync(string publicKey)
        {
            var balanceResult = await _rpcClient.GetBalanceAsync(new PublicKey(publicKey));
            if (balanceResult.WasSuccessful)
            {
                return balanceResult.Result.Value;
            }
            throw new Exception(balanceResult.Reason);
        }

        public async Task RequestAirdropAsync(string publicKey, ulong amount)
        {
            var airdropResult = await _rpcClient.RequestAirdropAsync(new PublicKey(publicKey), amount);
            if (!airdropResult.WasSuccessful)
            {
                throw new Exception(airdropResult.Reason);
            }

        }


        public async Task<List<SignatureStatusInfo>> GetTransactionHistoryAsync(string publicKey)
        {
            var transactionHistory = await _rpcClient.GetSignaturesForAddressAsync(new PublicKey(publicKey));
            if (transactionHistory.WasSuccessful)
            {
                return transactionHistory.Result;
            }
            throw new Exception(transactionHistory.Reason);
        }
    }
}
