using System.Net.Sockets;

namespace vMAPI.Network;

public class NetworkUtilities
{
    public static async Task<bool> TestConnectionAsync(string address, int port)
    {
        if (string.IsNullOrWhiteSpace(address))
        {
            return false;
        }

        var tcpClient = new TcpClient();
        try
        {
            await tcpClient.ConnectAsync(address, port);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            tcpClient.Close();
        }
    }
}
