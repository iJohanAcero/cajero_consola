using Microsoft.VisualBasic;
using System.IO;
using System.Text;
using System.Net.NetworkInformation;

namespace Cajero
{

    class Cajero
    {
        static string[,] usuario =    //MATRIZ DECLARADA CON VALORES YA EXISTENTES
                {
                    {"1010", "1010", "1000000"},
                    {"2020", "2020", "2000000"},
                    {"4040", "4040", "4000000"}
                };

        static void RegistroLOG(string mensaje)
        {
            try
            {
                //C:\Users\Johan\source\repos\CajeroCompleto\CajeroCompleto\bin\Debug\net8.0\registro.txt
                using (StreamWriter sr = File.AppendText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "registro.txt"))) //CREA EL ARCHIVO DE REGISTRO EN LA CARPETA DEBUG DEL PROYECTO
                {
                    sr.WriteLine($"{DateTime.Now}: {mensaje}"); //$ SIRVE PARA QUE SE PUEDA ESCRIBIR CODIGO DENTRO DEL TEXTO
                }

            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
            }

        }

        static void Main(string[] args)
        {
            RegistroLOG("********************************************************************************************");
            RegistroLOG("*****************************/NUEVO INICIO DE CONSOLA//***********************************");
            RegistroLOG("********************************************************************************************");
            while (true)//REPETIR HASTA UN BREAK
            {
                try
                {
                   

                    Console.WriteLine("CAJERO AUTOMATICO SENA");
                    Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - - -");

                    Console.WriteLine("DIGITE SU NUMERO DE CUENTA");
                    int cuenta = Convert.ToInt32(Console.ReadLine());
                    RegistroLOG("NUMERO DE CUENTA: "+cuenta);

                    Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
                    Console.WriteLine("DIGITE SU PIN");
                    string PIN = (Console.ReadLine());
                    RegistroLOG("PIN: "+PIN);
                    Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
                    int id = -1; //DECLARO QUE EL ID INICIA EN -1

                    //VALIDAR INFORMACION DENTRO DE LA MATRIZ PARA PODER ACCEDER AL MENU
                    //
                    for (int i = 0; i < usuario.GetLength(0); i++)
                    {

                        if (usuario[i, 1] == PIN && usuario[i, 0] == cuenta.ToString()) //VALIDA SI LA FILA I DE LA COLUMNA 1 ES IGUAL AL PIN
                                                                                        //Y USUARIO QUE ESTAN EN LA FILA I DE LA COLUMNA 0
                        {
                            id = i; //EL ID SE CONVIERTE EN 0
                            break;
                        }
                    }

                    if (id > -1) //YA QUE EL ID YA ES MAYOR A 0, ENTONCES ME LLEVA AL OTRO METODO
                    {
                        Console.Clear(); //LIMPIA LA CONSOLA
                        Menu(id, usuario); //MUESTRA LA OTRA FUNCION LLAMADA MENU
                        RegistroLOG($"INGRESA A LA FUNCION MENU CON ESTE ID: {id}");
                    }
                    else
                    {
                        Console.WriteLine("ERROR DE CREDENCIALES");
                        RegistroLOG("ERROR DE CREDENCIALES");
                    }

                }
                catch
                {
                    Console.WriteLine("ERROR DIGITE UN NUMERO");
                    RegistroLOG("ERROR DIGITE UN NUMERO");
                }
            }   
        }

        static void Menu(int id, string[,] usuario) //FUNCION MENU

        {
            while (true)
            {
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("BIENVENIDO AL CAJERO SENA");
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("1. RETIRAR DINERO");
                Console.WriteLine("2. CONSULTAR SALDO CUENTA CORRIENTE");
                Console.WriteLine("3. TRANSACCION A OTRAS CUENTAS");
                Console.WriteLine("4. CAMBIO DE PIN");
                Console.WriteLine("5. SALIR");
                int opci = int.Parse(Console.ReadLine());
                switch (opci)
                {
                    case 1:
                        RegistroLOG("RETIRAR DINERO");
                        retiro(id, usuario);
                        break;

                    case 2:
                        RegistroLOG("COSULTAR SALDO");
                        saldo(id, usuario);
                        break;

                    case 3:
                        RegistroLOG("TRANSFERIR DINERO");
                        Transferencia(id, usuario);
                        break;

                    case 4:
                        RegistroLOG("CAMBIAR PIN");
                        pin(id, usuario);
                        break;
                    case 5:
                        RegistroLOG($"SALIR DEL MENU DE LA CUENTA: {id}");
                        Console.Clear();
                        return;

                    default:
                        RegistroLOG("ERROR DIGITE UN NUMERO VALIDO");
                        Console.WriteLine("DIGITE UN NUMERO VALIDO");
                        break;
                }
            }

            static void saldo(int id, string[,] usuario)
            {
                Console.WriteLine("SU SALDO ES " + usuario[id, 2] + "COP"); //MUESTRA LA FILA Y COLUMNA ASIGNADA AL SALDO
                RegistroLOG($"SALDO DEL USUARIO: {usuario[id, 2]}");
            }

            
            static void retiro(int id, string[,] usuario)
            {
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("DIGITE CUANTO DESEA RETIRAR");
                int retiro2 = int.Parse(Console.ReadLine());

                int saldoactual = Convert.ToInt32(usuario[id, 2]); //CONVIERTO EL STRING DEL SALDO A INT

                if (retiro2 <= Convert.ToInt32(usuario[id, 2]))
                {

                    saldoactual -= retiro2; //LE RESTO LO RETIRADO AL SALDO
                    usuario[id, 2] = Convert.ToString(saldoactual);
                    Console.WriteLine("------------------------------------------------------------");
                    Console.WriteLine("USTED HA RETIRADO " + retiro2 + " COP DE SU CUENTA CORRIENTE, NUEVO SALDO ES " + usuario[id, 2] + " COP");
                    RegistroLOG($"USUARIO: {id} RETIRO: {retiro2} NUEVO SALDO: {usuario[id, 2]}");

                    saldo(id, usuario);//LLAMA LA FILA Y COLUMNA SALDO, LLAMANDO AL CONSULTAR SALDO
                }
                else
                {
                    Console.WriteLine("SALDO INSUFICIENTE PARA ESTE RETIRO");
                    RegistroLOG("SALDO INSUFICIETNE");
                }
            }

            static void Transferencia(int id, string[,] usuario)
            {
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("DIGITE CUANTO DESEA TRANSFERIR");
                int tran1 = int.Parse(Console.ReadLine());


                for (int i = 0; i < usuario.GetLength(0); i++)
                {
                    if (tran1 <= Convert.ToInt32(usuario[id, 2]))
                    {
                        Console.WriteLine("DIGITE EL NUMERO DE CUENTA DEL DESTINATARIO");
                        int trans = int.Parse(Console.ReadLine());

                        for (int j = 0; j < usuario.GetLength(0); j++)
                        {
                            if (trans == Convert.ToInt32(usuario[j, 0]))
                            {
                                usuario[id, 0] = Convert.ToString(trans);

                                Console.WriteLine("SE LE HA TRANSFERIDO " + tran1 + " COP AL USUARIO " + usuario[j, 0]);
                                RegistroLOG($"TRANSFERIDO: {tran1} DESTINATARIO: {usuario[j, 0]}");


                                // Actualiza el saldo 
                                int saldoactual = Convert.ToInt32(usuario[id, 2]); //DECLARO QUE SALDO ACTUAL ES EL SALDO DEL QUE REALIZA LA TRANSFERENCIA
                                saldoactual -= tran1; // RESTA LO TRANSFERIDO A SU CUENTA
                                usuario[id, 2] = Convert.ToString(saldoactual);
                                RegistroLOG($"ACTUALIZA SALDO DEL USUARIO: {id}");

                                // Actualiza el saldo del destinatario
                                int saldoDestinatario = Convert.ToInt32(usuario[j, 2]);//DECLARO QUE SALDODESTINATARIO ES A QUIEN LE LLEGA
                                saldoDestinatario += tran1; // SUMA LO TRANSFERIDO A SU CUENTA
                                usuario[j, 2] = Convert.ToString(saldoDestinatario);
                                RegistroLOG($"ACTUALIZA SALDO DEL DESTINATARIO: {usuario[j, 2]}");



                                saldo(id, usuario);
                                return;
                            }

                        }
                        Console.WriteLine("USUARIO NO EXISTE");
                        RegistroLOG("USUARIO NO EXISTE");
                        return;
                    }
                    else
                    {
                        Console.WriteLine("FONDOS INSUFICIENTES");
                        RegistroLOG("USUARIO NO EXISTE");
                        return;
                    }
                }
            }


            static void pin(int id, string[,] usuario)
            {
                Console.WriteLine("DIGITE SU CONTRASEÑA ACTUAL");
                int contra = int.Parse(Console.ReadLine()); //LEE LA CONTRASEÑA ACTUAL ASIGNADA EN LA MATRIZ
                RegistroLOG($"CONTRASEÑA ACTUAL: {contra}");

                if (contra == Convert.ToInt32(usuario[id, 1])) //VALIDA SI ESE PIN SE ENCUENTRA EN LA MATRIZ
                {
                    Console.WriteLine("DIGITE UNA NUEVA CONTRASEÑA");
                    int nueva = int.Parse(Console.ReadLine());
                    RegistroLOG($"NUEVA CONTRASEÑA: {nueva}");

                    usuario[id, 1] = Convert.ToString(nueva); //CAMBIA EL PIN GUARDADO EN LA MATRIZ POR LA NUEVA ESCRITA

                    Console.WriteLine("PIN CAMBBIADO EXITOSAMENTE");
                    RegistroLOG("PIN CAMBIADO EXITOSAMENTE");

                    Menu(id, usuario); //ME REGRESA AL MENU PARA VALIDAR EL PIN    
                }
                else
                {
                    Console.WriteLine("PIN INCORRECTO");
                    RegistroLOG("PIN INCORRECTO");
                }
            }
            
            static void salir(string[]args)
            {
                RegistroLOG("SALIR DEL MENU");
                return;
                
            }
        }
        }
    }