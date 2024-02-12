using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using ClosedXML.Excel;

namespace ControlDeHorasCanellaRV
{
    public partial class Principal : Form
    {
        private readonly string _KeyEnc = "Test1234";
        private readonly string CodigoEmpleado = "6213";
        private readonly string NombreColaborador = "Rodrigo Vasquez";
        private readonly String[] CodigoProyecto = new String[] { "N/A", "BEL App - 2" };
        private readonly String[] Proyectos = new String[] { "N/A","Automatización Validación Cuenta Digital (Wave 5461) Sprint 20",
            "REUNIONES INTERNAS", "VACACIONES", "CAPACITACIONES",
            "REPOSICIÓN DE HORAS","IGSS / VISITA MÉDICO","SIN ASIGNACIÓN","ASUETO / FERIADO"};
        private readonly String[] Etapa = new String[] { "N/A", "01 REQUERIMIENTO",
        "02 COTIZACIÓN","03 DERCAS","04 DESARROLLO","05 QA CANELLA","06 PRUEBAS TÉCNICAS",
            "07 PRUEBAS CERTIFICACIÓN","08 GARANTÓA","09 SOPORTE","10 CERRADO","00 CANCELADO"};
        private readonly String[] Actividad = new String[] { "N/A", "00 ANÓLISIS Y DISEÓO", "01 DERCAS DE USUARIO",
             "02 DERCAS TÓCNICO", "03 DESARROLLO", "04 DOCUMENTACIÓN", "05 QA CANELLA", "06 PRUEBAS TÉCNICAS",
             "07 PRUEBAS CERTIFICACIÓN", "08 REUNIONES CON CLIENTE", "09 SOPORTE A PRODUCCIÓN",
             "10 INSTALACIÓN", "11 SOPORTE A BANCO","12 REVISIÓN CÓDIGO","13 REUNIONES INTERNAS",
             "14 SOPORTE DE BANCAS ELECTRONICAS","15 HISTORICO","15 HISTORICO" };
        public Principal()
        {
            InitializeComponent();

        }

        private byte[] EncryptStringToBytes_Aes(string plainText, string keyString)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException(nameof(plainText));
            if (string.IsNullOrEmpty(keyString))
                throw new ArgumentException("La clave proporcionada es nula o vacÓa", nameof(keyString));

            byte[] encrypted;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = AdjustKeySize(keyString);
                aesAlg.GenerateIV();

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    // Prepend the IV to the ciphertext
                    msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
                    msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);

                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }

            return encrypted;
        }

        private string DecryptStringFromBytes_Aes(byte[] cipherTextWithIv, string keyString)
        {
            if (cipherTextWithIv == null || cipherTextWithIv.Length <= 0)
                throw new ArgumentNullException(nameof(cipherTextWithIv));
            if (string.IsNullOrEmpty(keyString))
                throw new ArgumentException("La clave proporcionada es nula o vacÓa", nameof(keyString));

            // Declarar la cadena para contener el texto desencriptado.
            string plaintext = null;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = AdjustKeySize(keyString);

                int ivLength = BitConverter.ToInt32(cipherTextWithIv, 0);
                byte[] iv = new byte[ivLength];
                Buffer.BlockCopy(cipherTextWithIv, sizeof(int), iv, 0, iv.Length);
                aesAlg.IV = iv;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Crea los flujos necesarios para la desencriptaciÓn.
                using (MemoryStream msDecrypt = new MemoryStream(cipherTextWithIv, sizeof(int) + iv.Length, cipherTextWithIv.Length - sizeof(int) - iv.Length))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Lee los bytes desencriptados del flujo de desencriptaciÓn y los coloca en una cadena.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime fechaInicioSemana = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            string nombreArchivo = $"Registros_{fechaInicioSemana:yyyyMMdd}.json.enc";
            RegistrosSemanales registrosSemanales;

            // Intenta leer y desencriptar el archivo existente
            if (File.Exists(nombreArchivo))
            {
                byte[] encryptedData = File.ReadAllBytes(nombreArchivo);
                string decryptedJsonString = DecryptStringFromBytes_Aes(encryptedData, _KeyEnc);
                registrosSemanales = System.Text.Json.JsonSerializer.Deserialize<RegistrosSemanales>(decryptedJsonString);
            }
            else
            {
                // Si el archivo no existe, inicializa una nueva lista de registros
                registrosSemanales = new RegistrosSemanales { FechaInicioSemana = fechaInicioSemana, Registros = new List<Registro>() };
            }

            // Crear un nuevo registro basado en los datos del formulario
            Registro nuevoRegistro = new Registro
            {
                Codigo = this.CodigoEmpleado,
                Colaborador = this.NombreColaborador,
                FechaRegistro = DateTime.Now,
                Proyecto = this.cb_Proyecto.Text,
                Etapa = this.cb_Etapa.Text,
                TipoActividad = this.cb_TipoActividad.Text,
                CantidadHoras = double.Parse(this.txt_CantidadHoras.Text.Replace(".", ",")),
                Descripcion = this.txt_Descripcion.Text

            };

            registrosSemanales.Registros.Add(nuevoRegistro);

            // Serializar la lista actualizada de registros a JSON
            string updatedJsonString = System.Text.Json.JsonSerializer.Serialize(registrosSemanales);

            // Encriptar y guardar el JSON actualizado
            byte[] updatedEncryptedData = EncryptStringToBytes_Aes(updatedJsonString, _KeyEnc);
            File.WriteAllBytes(nombreArchivo, updatedEncryptedData);
            this.txt_CantidadHoras.Text = "";
            this.txt_Descripcion.Text = "";
            this.cb_Proyecto.SelectedIndex = 0;
            this.cb_Etapa.SelectedIndex = 0;
            this.cb_TipoActividad.SelectedIndex = 0;
            CargarDatosYActualizarTotalHoras();
            ActualizarTotalHorasSemana();
            MessageBox.Show("Registro guardado con Exito.", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            this.cb_Proyecto.Items.AddRange(Proyectos);
            this.cb_Etapa.Items.AddRange(Etapa);
            this.cb_TipoActividad.Items.AddRange(Actividad);

            this.cb_Proyecto.SelectedIndex = 0;
            this.cb_Etapa.SelectedIndex = 0;
            this.cb_TipoActividad.SelectedIndex = 0;
            DateTime fechaInicioSemana = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            if (fechaInicioSemana == DateTime.Now.Date)
            {


                string nombreArchivo = $"Registros_{fechaInicioSemana:yyyyMMdd}.json.enc";
                if (!File.Exists(nombreArchivo))
                {
                    // Crear una instancia de tus datos semanales aquÓ
                    RegistrosSemanales datosSemanales = new RegistrosSemanales
                    {
                        FechaInicioSemana = fechaInicioSemana
                    };

                    // Serializa a JSON
                    string jsonString = JsonSerializer.Serialize(datosSemanales);

                    // Encripta el JSON
                    byte[] encryptedData = EncryptStringToBytes_Aes(jsonString, this._KeyEnc);

                    // Guarda el archivo encriptado
                    File.WriteAllBytes(nombreArchivo, encryptedData);

                    // Informar al usuario que se ha iniciado una nueva semana
                    MessageBox.Show("Se ha iniciado un nuevo registro para la semana.", "Nueva Semana", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            CargarDatosYActualizarTotalHoras();
            ActualizarTotalHorasSemana();
        }
        private static byte[] AdjustKeySize(string keyString, int size = 32)
        {
            if (string.IsNullOrEmpty(keyString))
                throw new ArgumentException("La clave no puede estar vacia");

            byte[] keyBytes = new byte[size];
            byte[] temporaryKeyBytes = Encoding.UTF8.GetBytes(keyString);

            Array.Copy(temporaryKeyBytes, keyBytes, Math.Min(temporaryKeyBytes.Length, size));

            return keyBytes;
        }
        private void CargarDatosYActualizarTotalHoras()
        {
            DateTime fechaInicioSemana = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            string nombreArchivo = $"Registros_{fechaInicioSemana:yyyyMMdd}.json.enc";

            // Inicializa la suma de horas a 0
            double sumaHorasHoy = 0;
            if (File.Exists(nombreArchivo))
            {
                try
                {
                    byte[] encryptedData = File.ReadAllBytes(nombreArchivo);
                    string decryptedJsonString = DecryptStringFromBytes_Aes(encryptedData, this._KeyEnc); // AsegÓrate de usar la clave correcta

                    // Deserializar JSON a la estructura de datos
                    RegistrosSemanales registrosSemanales = System.Text.Json.JsonSerializer.Deserialize<RegistrosSemanales>(decryptedJsonString);

                    // Calcular la suma de horas del dÓa actual
                    sumaHorasHoy = registrosSemanales?.Registros
                        .Where(r => r.FechaRegistro.Date == DateTime.Now.Date)
                        .Sum(r => r.CantidadHoras) ?? 0;
                }
                catch (Exception ex)
                {
                    // Manejo de errores (p.ej., archivo corrupto, problemas de deserializaciÓn)
                    MessageBox.Show($"Error al cargar o procesar el archivo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Archivo de registros de la semana no encontrado.", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Asignar la suma al control txt_TotalHorasDia
            txt_TotalHorasDia.Text = sumaHorasHoy.ToString("N2"); // "N2" para formatear con dos decimales
        }
        private void ActualizarTotalHorasSemana()
        {
            DateTime fechaInicioSemana = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            string nombreArchivo = $"Registros_{fechaInicioSemana:yyyyMMdd}.json.enc";

            double totalHorasSemana = 0;

            if (File.Exists(nombreArchivo))
            {
                try
                {
                    byte[] encryptedData = File.ReadAllBytes(nombreArchivo);
                    string decryptedJsonString = DecryptStringFromBytes_Aes(encryptedData, this._KeyEnc); // AsegÓrate de usar la misma clave

                    // Deserializar JSON a la estructura de datos
                    RegistrosSemanales registrosSemanales = System.Text.Json.JsonSerializer.Deserialize<RegistrosSemanales>(decryptedJsonString);

                    // Calcular la suma total de horas de la semana
                    totalHorasSemana = registrosSemanales?.Registros
                        .Sum(r => r.CantidadHoras) ?? 0;
                }
                catch (Exception ex)
                {
                    // Manejo de errores (p.ej., archivo corrupto, problemas de deserializaciÓn)
                    MessageBox.Show($"Error al cargar o procesar el archivo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Archivo de registros de la semana no encontrado.", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Asignar el total de horas de la semana al control txt_TotalHorasSemana
            txt_TotalHorasSemana.Text = totalHorasSemana.ToString("N2"); // "N2" para formatear con dos decimales
        }

        private void GenerarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime fechaInicioSemana = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            string nombreArchivo = $"Registros_{fechaInicioSemana:yyyyMMdd}.json.enc";
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            // Obtener el año y mes actual para las carpetas
            string yearFolderName = DateTime.Now.Year.ToString();
            string monthFolderName = DateTime.Now.ToString("MMMM", CultureInfo.CreateSpecificCulture("es")); // Asume que quieres el nombre del mes en español

            // Construir la ruta completa con las nuevas carpetas
            string finalFolderPath = Path.Combine(desktopPath, "ControlHoras", yearFolderName, monthFolderName);
            string pathToSave = Path.Combine(finalFolderPath, $"ControlDeHoras_{fechaInicioSemana:yyyyMMdd}.xlsx");

            // Verificar si la ruta de carpetas existe, sino, crearla
            if (!Directory.Exists(finalFolderPath))
            {
                Directory.CreateDirectory(finalFolderPath);
            }
            // Desencriptar y deserializar el archivo JSON
            RegistrosSemanales registrosSemanales;
            if (File.Exists(nombreArchivo))
            {
                byte[] encryptedData = File.ReadAllBytes(nombreArchivo);
                string decryptedJsonString = DecryptStringFromBytes_Aes(encryptedData, _KeyEnc);
                registrosSemanales = JsonSerializer.Deserialize<RegistrosSemanales>(decryptedJsonString);
            }
            else
            {
                MessageBox.Show("No se encontrÓ el archivo de registros de la semana.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Crear un nuevo libro de trabajo Excel
            using (var workbook = new ClosedXML.Excel.XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("CONTROL DE HORAS 2024");


                var image = worksheet.AddPicture("canella.png")
                    .MoveTo(worksheet.Cell(1, 1))
                    .Scale(0.4); // Ajusta esto segÓn el tamaÓo deseado de tu imagen

                // Combinar celdas para el tÓtulo "CONTROL DE HORAS 2024"
                worksheet.Range(1, 1, 3, 10).Merge().Value = "CONTROL DE HORAS 2024";
                worksheet.Range(1, 1, 3, 10).Style
                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                    .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                    .Font.SetBold()
                    .Font.FontSize = 11;

                string[] headers = { "Código", "Colaborador", "Jefe de Proyecto", "Código Proyecto", "Proyecto", "Fecha Registro", "Cantidad de horas", "Etapa", "Tipo de actividad", "Descripción" };
                for (int i = 0; i < headers.Length; i++)
                {
                    worksheet.Cell(4, i + 1).Value = headers[i]; // Comenzar en la fila 4
                    worksheet.Cell(4, i + 1).Style.Font.SetBold();
                    worksheet.Cell(4, i + 1).Style.Fill.SetBackgroundColor(XLColor.FromHtml("#EBF1DE"));
                    worksheet.Cell(4, i + 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                }

                // AÓadir los datos de los registros
                int currentRow = 5; // Comenzar en la fila 4
                foreach (var registro in registrosSemanales.Registros)
                {
                    string codigoProyecto = CodigoProyecto[0];
                    string jefeProyecto = "N/A";

                    if (registro.Proyecto == this.Proyectos[1])
                    {
                        codigoProyecto = CodigoProyecto[1];
                        jefeProyecto = "Luis Sanchez";
                    }
                    worksheet.Cell(currentRow, 1).Value = int.Parse(registro.Codigo);
                    worksheet.Cell(currentRow, 2).Value = registro.Colaborador;
                    worksheet.Cell(currentRow, 3).Value = jefeProyecto;
                    worksheet.Cell(currentRow, 4).Value = codigoProyecto;
                    worksheet.Cell(currentRow, 5).Value = registro.Proyecto;
                    worksheet.Cell(currentRow, 6).Value = registro.FechaRegistro.ToString("dd/MM/yyyy");
                    worksheet.Cell(currentRow, 7).Value = registro.CantidadHoras;
                    worksheet.Cell(currentRow, 8).Value = registro.Etapa;
                    worksheet.Cell(currentRow, 9).Value = registro.TipoActividad;
                    worksheet.Cell(currentRow, 10).Value = registro.Descripcion;

                    currentRow++;
                }

                // Formatear las celdas si es necesario (por ejemplo, ajustar anchos)
                worksheet.Columns().AdjustToContents();
                var range = worksheet.Range(worksheet.Cell(4, 1).Address, worksheet.Cell(currentRow - 1, headers.Length).Address);
                range.Cells().Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);


                // Guardar el libro de trabajo en un archivo
                workbook.SaveAs(pathToSave);

            }

            MessageBox.Show($"Archivo Excel generado en {pathToSave}.", "Generado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txt_CantidadHoras_TextChanged(object sender, EventArgs e)
        {
            DateTime fechaInicioSemana = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            string nombreArchivo = $"Registros_{fechaInicioSemana:yyyyMMdd}.json.enc";

            // Inicializa la suma de horas a 0
            double sumaHorasHoy = 0;

            if (File.Exists(nombreArchivo))
            {
                try
                {
                    byte[] encryptedData = File.ReadAllBytes(nombreArchivo);
                    string decryptedJsonString = DecryptStringFromBytes_Aes(encryptedData, this._KeyEnc); // AsegÓrate de usar la clave correcta

                    // Deserializar JSON a la estructura de datos
                    RegistrosSemanales registrosSemanales = System.Text.Json.JsonSerializer.Deserialize<RegistrosSemanales>(decryptedJsonString);

                    // Calcular la suma de horas del dÓa actual
                    sumaHorasHoy = registrosSemanales?.Registros
                        .Where(r => r.FechaRegistro.Date == DateTime.Now.Date)
                        .Sum(r => r.CantidadHoras) ?? 0;
                }
                catch (Exception ex)
                {
                    // Manejo de errores (p.ej., archivo corrupto, problemas de deserializaciÓn)
                    MessageBox.Show($"Error al cargar o procesar el archivo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Archivo de registros de la semana no encontrado.", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (DateTime.Now.DayOfWeek != DayOfWeek.Friday)
            {
                var restante = (9 - sumaHorasHoy).ToString("N2");
                if (double.Parse(restante) <= double.Parse(0.ToString("N2")))
                {
                    this.txt_HorasRestantes.Text = 0.ToString("N2");
                }
                else
                {
                    this.txt_HorasRestantes.Text = (9 - sumaHorasHoy).ToString("N2");
                }
            }
            else
            {
                var restante = (8 - sumaHorasHoy).ToString("N2");
                if (double.Parse(restante) <= double.Parse(0.ToString("N2")))
                {
                    this.txt_HorasRestantes.Text = 0.ToString("N2");
                }
                else
                {
                    this.txt_HorasRestantes.Text = (8 - sumaHorasHoy).ToString("N2");
                }
            }
        }

        private void cb_Proyecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cb_Proyecto.SelectedIndex == 2)
            {
                txt_Descripcion.Text = "Daily meeting,Daily Meeting,";
            }
        }
    }
}