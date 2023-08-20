using CommandLine;

namespace WIZEncrypt
{
    internal class CMDOptions
    {

        private string _input;
        private string _output;

        [Option('v', "verbose", Required = false, HelpText = "Enable verbose output.")]
        public bool Verbose { get; set; }

        [Option('p', "password", Required = true, HelpText = "password for Encryption and Decryption.")]
        public string Password { get; set; }

        [Option('s', "salt", Required = true, HelpText = "salt for Encryption and Decryption.")]
        public string Salt { get; set; }

        [Option('i', "in", Required = true, HelpText = "Input file path.")]
        public string Input
        {
            get => _input;
            set => _input = Path.GetFullPath(value);
        }

        [Option('o', "out", Required = false, HelpText = "Output file path.")]
        public string Output
        {
            get => _output = _output != null ? Path.GetFullPath(_output) : Path.GetFullPath(_input) + ".wizencrypted";
            set => _output = Path.GetFullPath(value);
        }
    }
}
