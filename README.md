# API DotNetEv Project v1

## Funcionalidades Principales

- Registro de nuevos usuarios con emisión de token JWT.
- Almacen de datos utilizando InMemory de (EF Core).
- Validación de credenciales para autenticación de usuarios.
- Encriptación de contraseñas mediante la biblioteca **BCrypt.Net-Next**.
- Validaciones robustas mediante **FluentValidation** y expresiones regulares.
- Consumo de endpoints externos a través de solicitudes HTTP (GET y POST).
- Documentación interactiva disponible vía **Swagger UI** y **Postman**.
- Arquitectura limpia basada en capas: controladores, servicios, modelos, validadores y utilidades.


## Tecnologías Utilizadas

- **.NET 8**
- **JWT (Json Web Token)**
- **BCrypt.Net-Next**
- **FluentValidation**
- **Swashbuckle (Swagger)**

  ## Estructura del Proyecto

```plaintext
├── API
│   └── Controllers
├── Application 
│   └── ManageServices
├── Common
│   ├── Utils 
├── EF
│   └── Context
├── Security
│   └── JWT 
```

## Endpoints Disponibles

- POST /api/CreateUser
- POST /api/ValidateUser
- POST /api/Client/SendDataToExternalURL
- GET /api/Client/ReadAnyEndpoint

## Seguridad
- Contraseñas encriptadas utilizando BCrypt.
- JWT incluyen fecha de expiración y se generan con una clave secreta definida en la configuración.

Para clonar el proyecto solo haz lo siguiente:

```plaintext
 git clone https://github.com/Yorestarling/DotNetEv.git
 cd DotNetEv
```
Autor
Desarrollado por [Yorestarling Mejia Beras].

