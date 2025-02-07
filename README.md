# Sistema de Autenticación en C# y WPF con MySQL

## Descripción
Este proyecto es un sistema de autenticación desarrollado en **C# y WPF**, que permite a los usuarios registrarse, iniciar sesión y recuperar su contraseña. Utiliza **MySQL** como base de datos

## Características
- **Registro de usuarios**: Permite crear una nueva cuenta en el sistema.
- **Inicio de sesión**: Verifica las credenciales y permite el acceso.
- **Recuperación de contraseña**: Opción para restablecer la contraseña en caso de olvido.
- **Seguridad**: Las contraseñas se almacenan de forma segura mediante **hashing**.
- **Uso de Variables de Entorno**: La conexión a la base de datos se configura mediante variables de entorno, evitando exponer credenciales sensibles en el repositorio.

## Tecnologías Utilizadas
- **Lenguaje**: C#
- **Interfaz gráfica**: WPF (Windows Presentation Foundation)
- **Base de datos**: MySQL
- **ORM/Conexión**: `MySql.Data.MySqlClient`

## Base de Datos
El sistema utiliza una base de datos llamada **`db_prueba`** en MySQL. Asegúrate de tenerla creada y configurada correctamente antes de ejecutar la aplicación.

## Estructura del Proyecto
```plaintext
Proyecto/
├── Helpers/            # Clases de ayuda
│
├── Models/            # Modelos de datos
│
├── Resources/         # Recursos gráficos y estilos
│   ├── images/        # Imágenes utilizadas en la interfaz
│   ├── styles/        # Archivos de estilos XAML
│
├── Services/          # Servicios del sistema
│
├── Views/             # Interfaces gráficas de usuario
│   ├── Login.xaml     # Pantalla de inicio de sesión
│   ├── Register.xaml  # Pantalla de registro
│   ├── Recovery.xaml  # Pantalla de recuperación de contraseña
```

## Configuración de la Base de Datos
La conexión a la base de datos se realiza mediante variables de entorno para mayor seguridad. Asegúrate de configurar las siguientes variables en tu entorno antes de ejecutar la aplicación:

```sh
export DB_HOST=tu_servidor
export DB_USER=tu_usuario
export DB_PASSWORD=tu_contraseña
export DB_NAME=db_prueba
```

Estas variables **no están incluidas en el repositorio** por razones de seguridad.

## Instalación y Uso
1. Clonar el repositorio:
   ```sh
   git clone https://github.com/tu-repositorio.git
   ```
2. Configurar las variables de entorno.
3. Restaurar paquetes de NuGet en el proyecto.
4. Compilar y ejecutar la aplicación en Visual Studio.

## Contribuciones
Si deseas contribuir, por favor sigue las siguientes normas:
- Realiza un **fork** del repositorio.
- Crea una **rama** con tu nueva funcionalidad.
- Envía un **pull request** para revisión.

## Licencia
Este proyecto está bajo la licencia **MIT**.


