using System;
using System.Media;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Security.Cryptography;

namespace ManyAlg01
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        //--------------------------------------------------------------Используемые методы---------------------------------------------------------------
        public BigInteger ExpModul_Method(long number, long power, ulong mod)
        {
            int count = 0;
            BigInteger result = 1;
            for (int i = 0; i < power; i++)
            {
                result *= number;
                count++;
            }
            return result = result % mod;
        }
        //----------------------------------------------------------------------------------------------
        public long GCD_Method(long n, long m)
        {
            long r = 0;
            long result = 0;
            if (m == 0) return r;
            for (int i = 0; i < m; i++)
            {
                result = m;
                r = n % m;
                n = m;
                m = r;
            }
            return result;
        }
        //----------------------------------------------------------------------------------------------
        public static BigInteger MultInv_Method(BigInteger number, BigInteger mod)
        {
            for (int i = 1; i < mod; i++)
            {
                if (number % mod * (i % mod) % mod == 1) 
                    return i;
            }
            return 1;
        }
        //----------------------------------------------------------------------------------------------
        public string TestFerma_Method(BigInteger num_testung, int num_random)
        {
            string result;
            if (BigInteger.Pow(num_testung, num_random - 1) % num_random == 1)
            {
                result = "Простое";
                return result;
            }
            else
            {
                result = "Не простое";
                return result;
            }
        }
        //----------------------------------------------------------------------------------------------
        public string TestMilleraRabinaa_Method(BigInteger num)
        {
            int k = 10;              //Можно назначить любое число раундов
            bool check = false;      //Проверка истина/ложь для возврата нужного значения

            if (num == 2 || num == 3)
            {
                check = true;
                if (check) return "Возможно простое";
                else return "Возможно не простое";
            }
            if (num < 2 || num % 2 == 0)
            {
                check = false;
                if (check) return "Возможно простое";
                else return "Возможно не простое";
            }

            // Предстановка num − 1 в виде (2^s)·t, где t нечётно, это делается последовательным делением num - 1 на 2
            BigInteger t = num - 1;
            int s = 0;
            while (t % 2 == 0)
            {
                t /= 2;
                s += 1;
            }

            for (int i = 0; i < k; i++)
            {
                // Выбор случайного целого числа a в отрезке [2, num − 2]
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                byte[] _a = new byte[num.ToByteArray().LongLength];
                BigInteger a;
                do
                {
                    rng.GetBytes(_a);
                    a = new BigInteger(_a);
                }
                while (a < 2 || a >= num - 2);

                BigInteger x = BigInteger.ModPow(a, t, num);

                if (x == 1 || x == num - 1)
                    continue;

                for (int r = 1; r < s; r++)
                {
                    x = BigInteger.ModPow(x, 2, num);

                    if (x == 1)
                    {
                        check = false;
                        if (check) return "Возможно простое";
                        else return "Возможно не простое";
                    }

                    if (x == num - 1)
                        break;
                }
                if (x != num - 1)
                {
                    check = false;
                    if (check) return "Возможно простое";
                    else return "Возможно не простое";
                }
            }
            check = true;

            if (check) return "Возможно простое";
            else return "Возможно не простое";
        }
        //----------------------------------------------------------------------------------------------
        public BigInteger BigNumGeneration_Method(int size)
        {
            Random rnd = new Random();
            BigInteger output = 1;
            int iteration = 1;
            while (true)
            {
                for (int i = 0; i < iteration; i++)
                {
                    output *= 2;
                }
                output -= 1;
                string temp = output.ToString();
                if (temp.Length >= size)
                {
                    return output;
                }
                iteration++;
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------

        //------------------------------------------------------Возведение числа в степень по модулю------------------------------------------------------
        private void BtnExpModul(object sender, RoutedEventArgs e)
        {
            SoundPlay();

            long number, power;
            ulong mod;
            try
            {
                number = Convert.ToInt64(StringFromRTB(numberPowerRTB));
                power = Convert.ToInt64(StringFromRTB(powerRTB));
                mod = Convert.ToUInt64(StringFromRTB(modul_powerRTB));
            }
            catch 
            {
                number = 1; power = 1; mod = 1;
            };

            StringInRTB(ExpModul_Method(number, power, mod).ToString(), resultPowerRTB);
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------

        //-----------------------------------------------------------------Вычисление НОД-----------------------------------------------------------------
        private void BtnGCD(object sender, RoutedEventArgs e)
        {
            SoundPlay();

            long num_n, num_m;
            try
            {
                num_n = Convert.ToInt64(StringFromRTB(number_nRTB));
                num_m = Convert.ToInt64(StringFromRTB(number_mRTB));
            }
            catch
            {
                num_n = 1; num_m = 1;
            };

            StringInRTB(GCD_Method(num_n, num_m).ToString(), result_nmRTB);
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------

        //------------------------------------------------------Вычисление мультипликативной инверсии-----------------------------------------------------
        private void BtnMultInv(object sender, RoutedEventArgs e)
        {
            SoundPlay();

            long num, mod;
            try
            {
                num = Convert.ToInt64(StringFromRTB(numberMultInvRTB));
                mod = Convert.ToInt64(StringFromRTB(modul_multinvRTB));
            }
            catch
            {
                num = 1; mod = 1;
            };

            StringInRTB(MultInv_Method(num, mod).ToString(), resultMultInvRTB);
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------

        //-------------------------------------------------------------------Тест Ферма-------------------------------------------------------------------
        private void BtnTestFerma(object sender, RoutedEventArgs e)
        {
            SoundPlay();

            BigInteger num_testing;
            int num_random;
            try
            {
                num_testing = BigInteger.Parse(StringFromRTB(number_testfermaRTB));
                num_random = Convert.ToInt32(StringFromRTB(number_randfermaRTB));
            }
            catch
            {
                num_testing = 1; num_random = 1;
            };

            StringInRTB(TestFerma_Method(num_testing, num_random), result_testfermaRTB);
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------

        //---------------------------------------------------------------Тест Миллера-Рабина--------------------------------------------------------------
        private void BtnTestMilleraRabina(object sender, RoutedEventArgs e)
        {
            SoundPlay();

            BigInteger num;
            try
            {
                num = BigInteger.Parse(StringFromRTB(number_MRtestRTB));
            }
            catch
            {
                num = 1;
            };

            StringInRTB(TestMilleraRabinaa_Method(num), result_MRtestRTB);
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------

        //--------------------------------------------------------Генерация простого большого числа-------------------------------------------------------
        private void BtnBigNumGeneration(object sender, RoutedEventArgs e)
        {
            SoundPlay();

            int size;
            try
            {
                size = Convert.ToInt32(StringFromRTB(sizeRTB));
            }
            catch
            {
                size = 1;
            };

            StringInRTB(BigNumGeneration_Method(size).ToString(), result_bignumberRTB1);
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------Функционал------------------------------------------------------------------
        private string StringFromRTB(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(
                rtb.Document.ContentStart,
                rtb.Document.ContentEnd
            );
            return textRange.Text;
        }
        private void StringInRTB(string received_str, RichTextBox rtb)
        {
            FlowDocument objFdoc = new FlowDocument();
            Paragraph objPara1 = new Paragraph();
            objPara1.Inlines.Add(new Run(received_str));
            objFdoc.Blocks.Add(objPara1);
            rtb.Document = objFdoc;
        }
        //----------------------------------------------------------------------------------------------
        private void SoundPlay()
        {
            try
            {
                SoundPlayer sp = new SoundPlayer();
                sp.SoundLocation = @"MyData\Sounds\sound_btn.wav";
                sp.Load();
                sp.Play();
            }
            catch (Exception er)
            {
                Console.WriteLine("Exception: " + er.Message);
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------
    }
}