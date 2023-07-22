using NetworkUtility.DNS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace NetworkUtility.Ping;
public class NetworkService
{
    private readonly IDNS _dns;
    public NetworkService(IDNS dNS)
    {
        _dns = dNS;
    }

    public string SendPing()
    {
        var dnsResult = _dns.SendDNSRequest();
        if (dnsResult is true)
        {
            return "Success: Ping Sent!";
        }
        else
        {
            return "Failure: Ping Not Sent!";
        }
    }

    public int PingTimeOut(int a, int b)
    {
        return a + b;
    }

    public DateTime LastPingDate()
    {
        return DateTime.Now;
    }

    public PingOptions GetPingOptions()
    {
        return new PingOptions()
        {
            DontFragment = true,
            Ttl = 1
        };
    }

    public IEnumerable<PingOptions> GetMostRecentPings()
    {
        IEnumerable<PingOptions> pingOptions = new[]
        {
            new PingOptions()
            {
                DontFragment = true,
                Ttl = 1
            },
            new PingOptions()
            {
                DontFragment = true,
                Ttl = 1
            },
            new PingOptions()
            {
                DontFragment = true,
                Ttl = 1
            }
        };

        return pingOptions;
    }
}
