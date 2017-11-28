using System;
using System.Net.Sockets;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace FlyerMe
{
    /// -----------------------------------------------------------------------------
    ///<summary>
    /// PayJunction class - offer functions and routines for payment processing
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <history>
    /// </history>
    /// -----------------------------------------------------------------------------
    [Serializable]
    public class PayJunction
    {
        public void ProcessPayment(String url, String urlArgs, out string result, out string transactionid)
        {
            Stream requestStream = null;
            WebResponse response = null;
            StreamReader reader = null;

            result = "failure";
            transactionid = string.Empty;

            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Method = WebRequestMethods.Http.Post;
                request.ContentType = "application/x-www-form-urlencoded";
                StringBuilder urlEncoded = new StringBuilder();
                Char[] reserved = { '?', '=', '&' };
                byte[] byteBuffer = null;

                if (urlArgs != null)
                {
                    int i = 0, j;
                    while (i < urlArgs.Length)
                    {
                        j = urlArgs.IndexOfAny(reserved, i);
                        if (j == -1)
                        {
                            urlEncoded.Append(HttpUtility.UrlEncode(urlArgs.Substring(i, urlArgs.Length - i)));
                            break;
                        }
                        urlEncoded.Append(HttpUtility.UrlEncode(urlArgs.Substring(i, j - i)));
                        urlEncoded.Append(urlArgs.Substring(j, 1));
                        i = j + 1;
                    }
                    byteBuffer = Encoding.UTF8.GetBytes(urlEncoded.ToString());
                    request.ContentLength = byteBuffer.Length;
                    requestStream = request.GetRequestStream();
                    requestStream.Write(byteBuffer, 0, byteBuffer.Length);
                    requestStream.Close();
                }
                else
                {
                    request.ContentLength = 0;
                }

                response = request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                reader = new StreamReader(responseStream, new ASCIIEncoding());

                string httpResponse = reader.ReadToEnd();
                string _transaction_id = string.Empty;
                string _response_code = string.Empty;

                if (httpResponse.Length > 0)
                {
                    Char delimiter = '\x001c';
                    string[] responseCodes = httpResponse.Split(delimiter);

                    //get transaction id
                    delimiter = '=';
                    string[] temp = responseCodes[0].Split(delimiter);
                    _transaction_id = temp[1].ToString();
                    transactionid = _transaction_id;

                    //get response code
                    delimiter = '=';
                    temp = responseCodes[1].Split(delimiter);
                    _response_code = temp[1].ToString();
                }

                if (_response_code == "85" || _response_code == "00") { result = "success"; }
                if (_response_code == "ZE") { result = "Address verification failed because zip did not match."; }
                if (_response_code == "XE") { result = "Address verification failed because zip and address did not match."; }
                if (_response_code == "YE") { result = "Address verification failed because zip and address did not match."; }
                if (_response_code == "OE") { result = "Address verification failed because address or zip did not match."; }
                if (_response_code == "UE") { result = "Address verification failed because cardholder address unavailable."; }
                if (_response_code == "RE") { result = "Address verification failed because address verification system is not working."; }
                if (_response_code == "SE") { result = "Address verification failed because address verification system is unavailable."; }
                if (_response_code == "EE") { result = "Address verification failed because transaction is not a mail or phone order."; }
                if (_response_code == "GE") { result = "Address verification failed because international support is unavailable."; }
                if (_response_code == "CE") { result = "Declined because CVV2/CVC2 code did not match."; }
                if (_response_code == "NL") { result = "Aborted because of a system error, please try again later."; }
                if (_response_code == "AB") { result = "Aborted because of an upstream system error, please try again later."; }
                if (_response_code == "04") { result = "Declined. Pick up card."; }
                if (_response_code == "07") { result = "Declined. Pick up card (Special Condition)."; }
                if (_response_code == "41") { result = "Declined. Pick up card (Lost)."; }
                if (_response_code == "43") { result = "Declined. Pick up card (Stolen)."; }
                if (_response_code == "13") { result = "Declined because of the amount is invalid."; }
                if (_response_code == "14") { result = "Declined because the card number is invalid."; }
                if (_response_code == "80") { result = "Declined because of an invalid date."; }
                if (_response_code == "05") { result = "Declined. Do not honor."; }
                if (_response_code == "51") { result = "Declined because of insufficient funds."; }
                if (_response_code == "N4") { result = "Declined because the amount exceeds issuer withdrawal limit."; }
                if (_response_code == "61") { result = "Declined because the amount exceeds withdrawal limit."; }
                if (_response_code == "62") { result = "Declined because of an invalid service code (restricted)."; }
                if (_response_code == "65") { result = "Declined because the card activity limit exceeded."; }
                if (_response_code == "93") { result = "Declined because there a violation (the transaction could not be completed)."; }
                if (_response_code == "06") { result = "Declined because address verification failed."; }
                if (_response_code == "54") { result = "Declined because the card has expired."; }
                if (_response_code == "15") { result = "Declined because there is no such issuer."; }
                if (_response_code == "96") { result = "Declined because of a system error."; }
                if (_response_code == "N7") { result = "Declined because of a CVV2/CVC2 mismatch."; }
                if (_response_code == "M4") { result = "Declined."; }

            }
            catch (UriFormatException ex)
            {
                result = ex.Message;
            }
            catch (WebException ex)
            {
                result = ex.Message;
            }
            catch (IOException ex)
            {
                result = ex.Message;
            }
            finally
            {
                if (requestStream != null)
                    requestStream.Close();
                if (response != null)
                    response.Close();
                if (reader != null)
                    reader.Close();
            }
        }// End ProcessPayment


    }// end of the class
}
