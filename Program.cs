using CommandLine;
using WIZEncrypt;

internal class Program
{
    static void Main(string[] args)
    {
        Parser.Default.ParseArguments<CMDOptions>(args)
            .WithParsed(RunWithOptions);
    }

    static void RunWithOptions(CMDOptions options)
    {
        if (options.Verbose)
        {
            Console.WriteLine("Verbose mode enabled.");
        }

        if (options.Output == null)
        {
            string outputFileName = options.Input.EndsWith(".wizencrypted") ?
                                    options.Input.Replace(".wizencrypted", "") :
                                    options.Input + ".wizencrypted";

            if (options.Input.EndsWith(".wizencrypted"))
            {
                EncryptionUtility.DecryptFile(options.Input, outputFileName, options.Password, options.Salt);
            }
            else
            {
                EncryptionUtility.EncryptFile(options.Input, outputFileName, options.Password, options.Salt);
            }
        }
        else
        {
            if (options.Input.EndsWith(".wizencrypted"))
            {
                EncryptionUtility.DecryptFile(options.Input, options.Output, options.Password, options.Salt);
            }
            else
            {
                EncryptionUtility.EncryptFile(options.Input, options.Output, options.Password, options.Salt);
            }
        }
    }
}