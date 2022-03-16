﻿using System;
using System.Text;
using System.IO;

namespace AutomaticProcesses
{
    class Program
    {
        // This variable is for knowing if the app is running in Debug mode or not.
        private static bool pDebug;

        private static cMiscFunctions.eFileType _fileType;
        private static cMiscFunctions.eOrientation _orientation;

        static void Main(string[] args)
        {
#if DEBUG
            pDebug = true;
#else
            pDebug = false;
#endif
            string _stage;
            string _currentArgName, _currentArgValue;
            string _DBuser = "", _DBpassword = "", _DBServer = "", _DBdataBase = "";
            string _mailServer = "", _mailUser = "", _mailPassword = "";
            string _processQuery = "", _processQueryParams = "", _processMailTo = "", _processMailSubject = "";
            bool _noBand = false, _noEmpty = false;
            string _html = "", _fileName = "";
            string _myName = System.Reflection.Assembly.GetCallingAssembly().GetName().Name;

            // Args
            _stage = "Checking args";
            try
            {
                Console.WriteLine($"----==== Starting [{_myName}] at {System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} ====----");

                try
                {
                    // If the settings file exists, the params will be loaded from it
                    _stage = "Loading settings file";
                    //string[] lines = File.ReadAllLines($"../.{_myName}.creds");
                    string[] _lines = File.ReadAllLines((pDebug?Directory.GetCurrentDirectory().Substring(0,3): $"/media/bin/{_myName}/")+$"{_myName}.settings", Encoding.Unicode);

                    //
                    _stage = "Getting settings from file";
                    foreach (string _line in _lines)
                    {
                        // Get the arg name and value
                        _currentArgName = _line.Split('=')[0].ToUpper();
                        _currentArgValue = _line.Split('=')[1];

                        // Identify arg name
                        switch (_currentArgName)
                        {
                            case "DB_SERVER":
                                _DBServer = _currentArgValue;
                                break;
                            case "DB_USER":
                                _DBuser = _currentArgValue;
                                break;
                            case "DB_PASSWORD":
                                _DBpassword = _currentArgValue;
                                break;
                            case "DB_DATABASE":
                                _DBdataBase = _currentArgValue;
                                break;
                            case "MAIL_SERVER":
                                _mailServer= _currentArgValue;
                                break;
                            case "MAIL_USER":
                                _mailUser= _currentArgValue;
                                break;
                            case "MAIL_PASSWORD":
                                _mailPassword = _currentArgValue;
                                break;
                            default:
                                throw new Exception($"Wrong argument: {_currentArgName}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Main#{_stage}] {ex.Message}");
                    Console.WriteLine($"----==== Ending [{_myName}] at {System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} ====----");
                    return;
                }

                // If params are set, they will override those loaded from the settings file
                _stage = "Getting settings from args";
                foreach (string arg in args)
                {
                    // Get the arg name and value
                    _currentArgName = arg.Split('=')[0].ToUpper();
                    if (arg.Split('=').Length == 2)
                        _currentArgValue = arg.Split('=')[1];
                    else if (arg.Split('=').Length > 2)
                        throw new Exception($"Wrong argument: {arg}");
                    else
                        _currentArgValue = "";
                    
                    // Identify arg name
                    switch (_currentArgName.ToUpper())
                    {
                        case "DB_SERVER":
                            _DBServer = _currentArgValue;
                            break;
                        case "DB_USER":
                            _DBuser = _currentArgValue;
                            break;
                        case "DB_PASSWORD":
                            _DBpassword = _currentArgValue;
                            break;
                        case "DB_DATABASE":
                            _DBdataBase = _currentArgValue;
                            break;
                        case "MAIL_SERVER":
                            _mailServer = _currentArgValue;
                            break;
                        case "MAIL_USER":
                            _mailUser = _currentArgValue;
                            break;
                        case "MAIL_PASSWORD":
                            _mailPassword = _currentArgValue;
                            break;
                        case "QUERY":
                            _processQuery = _currentArgValue;
                            break;
                        case "PARAMS":
                            _processQueryParams = _currentArgValue;
                            break;
                        case "TO":
                            _processMailTo = _currentArgValue;
                            break;
                        case "SUBJECT":
                            _processMailSubject = _currentArgValue;
                            break;
                        case "NOBAND":
                            _noBand = true;
                            break;
                        case "NOEMPTY":
                            _noEmpty = true;
                            break;
                        case "FILENAME":
                            _fileName = _currentArgValue;
                            break;
                        case "FILETYPE":
                            Enum.TryParse(_currentArgValue, out cMiscFunctions.eFileType _fType);
                            _fileType = _fType;
                            break;
                        case "ORIENTATION":
                            Enum.TryParse(_currentArgValue, out cMiscFunctions.eOrientation _orien);
                            _orientation = _orien;
                            break;

                        default:
                            throw new Exception($"Wrong argument: {_currentArgName}");
                    }
                }

                // Check mandatory arguments

            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Main#{_stage}] {ex.Message}");
                Console.WriteLine($"----==== Ending [{_myName}] at {System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} ====----");
                return;
            }


            cCredentials _credsDB = new cCredentials(_DBServer,_DBuser,_DBpassword,_DBdataBase);
            cCredentials _credsMail = new cCredentials(_mailServer, _mailUser, _mailPassword, "");

            // Check the query number

            cProcess _cp = new cProcess(_credsDB, Convert.ToInt32(_processQuery), _processQueryParams, _processMailSubject, _noBand, _fileName, _fileType, _orientation);
            _html = _cp.Process();
            if(!_cp.Error && (_html!="" || !_noEmpty))
            {
                ExchangeAttachments _email = new ExchangeAttachments();
                _email.Connect(_credsMail);


                _stage = "Sending email";
                if (!_email.SendEmail(_processMailTo, _processMailSubject+DateTime.Now.ToString(" dd/MM/yyyy"), _html!=""?_html: "<html><body>No results found / No se encontraron resultados</body></html>",_cp.FileName))
                    throw new Exception("Could not send the email.");

                //
                _stage = "Disconnecting";
                _email.Dispose();
            }




            Console.WriteLine($"> Parameters: {String.Join(" /", args) }");

            Console.WriteLine($"----==== Ending [{_myName}] at {System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} ====----");
        }

    }
}