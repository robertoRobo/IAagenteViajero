using IA.estructura;
using System;
using System.Collections.Generic;

namespace IA
{
    class Program
    {
        static List<ciudad> ciudadesBase;
        static double[] distacias = new double[100];
        static double[] distaciasHijos = new double[100];
        static int[] posiciones = new int[26];

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                ciudadesBase = new List<ciudad>();
                generarVectorBase(ciudadesBase);
                int i = 0;
                List<ciudad>[] c = new List<ciudad>[100];
                List<ciudad>[] nueva_generacion = new List<ciudad>[100];
                //double[] distacias = new double[100];
                while (i < 100)
                {
                    List<ciudad> v = vectoresRandom(ciudadesBase);
                    c[i] = new List<ciudad>(v);
                    
                    Boolean resp = repetidos(c, i);
                    if (resp)
                    {
                        Console.WriteLine("vector igual " + i);
                    }
                    else
                    {
                        i += 1;
                    }
                }
                distacias = raices(c);
                distacias = MetodoBurbuja(c);
                for (int x = 0; x < 100; x++)
                {
                    for (int y = 0; y < 26; y++)
                    {
                        Console.Write(c[x][y].nombre + "-");
                    }
                    nueva_generacion[x] = new List<ciudad>(c[x]);
                    Console.Write("## " + distacias[x]);
                    Console.WriteLine();
                }
                generacionN(nueva_generacion,c);
                Console.ReadLine();
                distacias = new double[100];
                c = null;
                nueva_generacion = null;
            }
        }
        //funcion principal para la creacion de una generacionNueva
        public static void generacionN(List<ciudad>[] nuevos, List<ciudad>[] ciudades) {
            Console.WriteLine("Nuevos");
            for (int y = 0; y < 25; y++) {
                nuevos[y][1] = ciudades[y][1];
                nuevos[y][24] = ciudades[y][24];
                nuevos[y] = CombinarPadres(ciudades[y],ciudades[y+1],nuevos[y]);
                imprimeHijos(ciudades[y], ciudades[y + 1], nuevos[y]);
               
                Console.WriteLine("");

                nuevos[y+1][1] = ciudades[y][1];
                nuevos[y+1][24] = ciudades[y][24];
                nuevos[y+1] = CombinarPadres(ciudades[y], ciudades[y + 2], nuevos[y+1]);
                imprimeHijos(ciudades[y], ciudades[y + 2], nuevos[y + 1]);

                Console.WriteLine("");

                nuevos[y+2][1] = ciudades[y][1];
                nuevos[y+2][24] = ciudades[y][24];
                nuevos[y+2] = CombinarPadres(ciudades[y], ciudades[y + 3], nuevos[y+2]);
                imprimeHijos(ciudades[y], ciudades[y + 3], nuevos[y + 2]);

                Console.WriteLine("");

                nuevos[y+3][1] = ciudades[y][1];
                nuevos[y+3][24] = ciudades[y][24];
                nuevos[y+3] = CombinarPadres(ciudades[y], ciudades[y + 4], nuevos[y+3]);
                imprimeHijos(ciudades[y], ciudades[y + 4], nuevos[y + 3]);

                Console.WriteLine("");
            }
            distaciasHijos = raices(nuevos);
            distaciasHijos = MetodoBurbuja(nuevos);
            for (int y = 0; y < 100;y++)
            {
                for (int x = 0; x < 26; x++)
                {
                    Console.Write(nuevos[y][x].nombre + "-");
                }
                Console.Write("# "+distaciasHijos[y]);
                Console.WriteLine("");
            }
            Console.WriteLine("");
        }
        //solo imprime en el formato padre1,padre2,nuevoHijo
        public static void imprimeHijos(List<ciudad> padre1, List<ciudad> padre2, List<ciudad> nuevos)
        {
            for (int x = 0; x < 26; x++)
            {
                Console.Write(posiciones[x] + "(" + x + ")" + padre1[x].nombre + "-");
            }
            Console.WriteLine("");
            for (int x = 0; x < 26; x++)
            {
                Console.Write(posiciones[x] + "(" + x + ")" + padre2[x].nombre + "-");
            }
            Console.WriteLine("");
            for (int x = 0; x < 26; x++)
            {
                Console.Write("(" + x + ")" + nuevos[x].nombre + "-");
            }
            Console.Write("#");
            Console.WriteLine("");
        }
        public static List<ciudad> CombinarPadres(List<ciudad> padre1, List<ciudad> padre2, List<ciudad> nuevos) {
            Boolean fin = true;
            //este ciclo inicializa en 0 cada elemento del nuevo hijo
            for (int x = 2; x < 24; x++) {
                nuevos[x] = new ciudad() { nombre = "c0" };
            }
            //este vector de posiciones sera para marcar por donde ha pasado en el ciclo while
            posiciones = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            posiciones[1] = 1;
            if (padre1[1].nombre.CompareTo(padre2[1].nombre) != 0)
            {
                int pos = 1;
                //este while genera el recorrido de coincidencias
                while (fin)
                {
                    pos = encontrarPosicion(padre1, padre2[pos].nombre);
                    if (pos == 1)
                    {
                        posiciones[pos] = 1;
                        fin = false;
                    }
                    else if (pos == 24)
                    {
                        posiciones[pos] = 1;
                        fin = false;
                    }
                    else
                    {
                        posiciones[pos] = 1;
                    }
                }
                for (int x = 2; x < 24; x++) {
                    if(posiciones[x] == 1)
                    {
                        nuevos[x] = padre1[x];
                    }
                    else if(posiciones[x] == 0)
                    {
                        /*int bus = encontrarPosicion(nuevos, padre2[x].nombre);
                        //int bus = NoExiste(nuevos,padre2);
                        if ( bus != -1)
                        {
                            nuevos[x] = padre2[x];
                        }
                        else {
                            int busw = NoExiste(nuevos, padre2);
                            nuevos[x] = padre2[busw];
                        }*/
                        
                        int enc = -1;
                        //for (int x = 1; x < 25; x++)
                        //{
                            //if (posiciones[x] == 1)
                            //{
                        for (int y = 1; y < 25; y++)
                        {
                            if (nuevos[y].nombre.CompareTo(padre2[x].nombre) != 0)
                            {
                                enc = y;
                            }
                            else
                            {
                                enc = -1;
                                break;
                            }
                        }
                        if (enc == -1)
                        {
                            int position = NoExiste(nuevos, padre2);
                            if(position != -1)
                            {
                                nuevos[x] = padre2[position];   
                            }
                            //nuevos[x] = padre2[position];
                        }
                        else {
                            nuevos[x] = padre2[x];
                        }
                            //}
                        //}

                    }
                }

            }
            else if (padre1[1].nombre.CompareTo(padre2[1].nombre) == 0)
            {
                for (int x = 2; x < 24; x++)
                {
                    nuevos[x] = padre2[x];
                }
            }
            return nuevos;
        }
        //esta funcion busca en los elementos marcados cual no existe aun en el hijo y lo inserta en el nuevo hijo
        public static int NoExiste(List<ciudad> nuevo, List<ciudad> padre2) {
            int enc = -1;
            for (int x = 1; x <25; x++)
            {
                if (posiciones[x] == 1 ) {
                    for (int y = 1; y < 25; y++)
                    {
                        if (nuevo[y].nombre.CompareTo(padre2[x].nombre) != 0)
                        {
                            
                            enc = x;
                        }
                        else {
                            
                            enc = -1;
                        }
                        if (enc == -1)
                        {
                            break;
                        }
                    }
                }
            }
            return enc;
        }
        //funcion que retorna la posicion de la ciudad en un vector, y retorna -1 si no se encuentra
        public static int encontrarPosicion(List<ciudad> padre1,string buscar) {
            int posicion = -1;
            for (int x = 0; x < 26; x++) {
                if (padre1[x].nombre.CompareTo(buscar)==0) {
                    posicion = x;
                    return posicion;
                }
            }
            return posicion;
        }
        //calcula las distancias entre ciudades y retorna un vector con los resultados
        public static double[] raices(List<ciudad>[] ciudades) {
            for (int x = 0; x < 100; x++)
            {
                double sumatoria = 0;
                for (int y = 0; y < 25; y++) {
                    sumatoria += Math.Sqrt(Math.Pow((ciudades[x][y].x - ciudades[x][y+1].x),2)+Math.Pow((ciudades[x][y].y - ciudades[x][y + 1].y), 2));
                    /*if (x == 0) {
                        //Console.Write("Sqrt(["+ ciudades[x][y].x+"-"+ ciudades[x][y + 1].x+"]^ +["+ ciudades[x][y].y +"-"+ ciudades[x][y + 1].y+"])");
                    }*/
                }
                distacias[x] = sumatoria;
                //Console.WriteLine(distacias[x]);
            }
            return distacias;
        }
        //ordena la lista de vectores de ciudades
        public static double[] MetodoBurbuja(List<ciudad>[] ciudades)
        {
            double t;
            List<ciudad> copia;
            for (int a = 1; a < distacias.Length; a++)
                for (int b = distacias.Length - 1; b >= a; b--)
                {
                    if (distacias[b - 1] > distacias[b])
                    {
                        copia = ciudades[b - 1];
                        t = distacias[b - 1];
                        distacias[b - 1] = distacias[b];
                        ciudades[b - 1] = ciudades[b];
                        distacias[b] = t;
                        ciudades[b] = copia;
                    }
                }
            return distacias;
        }
        //Genera nuevos vectores apartir de una base
        public static List<ciudad> vectoresRandom(List<ciudad> input)
        {
            List<ciudad> arr = input;
            //List<ciudad> copia = input;
            List<ciudad> arrDes = new List<ciudad>();
            arrDes.Add(arr[0]);
            arr.RemoveAt(0);
            Random randNum = new Random();
            //Console.WriteLine("#####tamaño del vector: "+arr.Count);
            while (arr.Count > 0)
            {
                int val = randNum.Next(0, arr.Count );
                if (arr[val].nombre.CompareTo("c1") != 0) 
                {
                    //Console.WriteLine(val+"###");
                    arrDes.Add(arr[val]);
                    arr.RemoveAt(val);
                } else if (arr.Count == 1) {
                    //Console.WriteLine("#fin#");
                    arrDes.Add(arr[val]);
                    arr.RemoveAt(val);
                }
            }
           
            ciudadesBase = arrDes;
            return arrDes;
        }
        //aqui se genera la primer lista del vector base
        public static List<ciudad> generarVectorBase(List<ciudad> ciudades) {
            ciudades.Add(new ciudad() { nombre = "c1", x = 5, y = 4 , ciclo = 0});
            ciudades.Add(new ciudad() { nombre = "c2", x = 7, y = 4, ciclo = 0 });
            ciudades.Add(new ciudad() { nombre = "c3", x = 5, y = 6, ciclo = 0 });
            ciudades.Add(new ciudad() { nombre = "c4", x = 4, y = 3, ciclo = 0 });
            ciudades.Add(new ciudad() { nombre = "c5", x = 3, y = 6, ciclo = 0 });
            ciudades.Add(new ciudad() { nombre = "c6", x = 4, y = 5, ciclo = 0 });
            ciudades.Add(new ciudad() { nombre = "c7", x = 2, y = 4, ciclo = 0 });
            ciudades.Add(new ciudad() { nombre = "c8", x = 1, y = 3, ciclo = 0 });
            ciudades.Add(new ciudad() { nombre = "c9", x = 1, y = 5, ciclo = 0 });
            ciudades.Add(new ciudad() { nombre = "c10", x = 3, y = 2, ciclo = 0 });
            ciudades.Add(new ciudad() { nombre = "c11", x = 6, y = 3, ciclo = 0 });
            ciudades.Add(new ciudad() { nombre = "c12", x = 7, y = 7, ciclo = 0 });
            ciudades.Add(new ciudad() { nombre = "c13", x = 6, y = 1, ciclo = 0 });
            ciudades.Add(new ciudad() { nombre = "c14", x = 4, y = 1, ciclo = 0 });
            ciudades.Add(new ciudad() { nombre = "c15", x = 1, y = 1, ciclo = 0 });
            ciudades.Add(new ciudad() { nombre = "c16", x = 1, y = 7, ciclo = 0 });
            ciudades.Add(new ciudad() { nombre = "c17", x = 4, y = 7, ciclo = 0 });
            ciudades.Add(new ciudad() { nombre = "c18", x = 7, y = 2, ciclo = 0 });
            ciudades.Add(new ciudad() { nombre = "c19", x = 9, y = 2, ciclo = 0 });
            ciudades.Add(new ciudad() { nombre = "c20", x = 8, y = 5, ciclo = 0 });
            ciudades.Add(new ciudad() { nombre = "c21", x = 10, y = 4, ciclo = 0 });
            ciudades.Add(new ciudad() { nombre = "c22", x = 11, y = 1, ciclo = 0 });
            ciudades.Add(new ciudad() { nombre = "c23", x = 10, y = 7, ciclo = 0 });
            ciudades.Add(new ciudad() { nombre = "c24", x = 13, y = 6, ciclo = 0 });
            ciudades.Add(new ciudad() { nombre = "c25", x = 12, y = 8, ciclo = 0 });
            ciudades.Add(new ciudad() { nombre = "c1", x = 5, y = 4, ciclo = 0 });
            return ciudades;
        }
        //checa si existe ya un vector
        public static Boolean repetidos(List<ciudad>[] ciudades,int i) {
            Boolean encontrado = false;
            if (i > 0)
            {
                for (int x = 0; x < i; x++)
                {
                    for (int y = 0; y < ciudades[x].Count; y++)
                    {

                        if (ciudades[i][y].nombre.CompareTo(ciudades[x][y].nombre) != 0)
                        {
                            encontrado = false;
                            return false;
                        }
                        else
                        {
                            encontrado = true;
                        }
                        //Console.Write(ciudades[x][y].nombre + "--");
                    }
                    if (encontrado == true)
                    {
                        return true;
                    }
                }
            }
            
            return encontrado;
        }
    }
}
