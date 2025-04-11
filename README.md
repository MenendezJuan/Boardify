# Boardify

Boardify es una API en .NET 8 que proporciona funcionalidades para gestionar tableros, listas y tarjetas al estilo de Trello.

## Installation

Para instalar y ejecutar Boardify localmente, sigue estos pasos:

1. **Clonar el repositorio:**

    ```sh
    https://github.com/damianrodriguez88/TrelloApi.git
    cd TrelloApi
    ```

2. **Restaurar los paquetes NuGet:**

    ```sh
    dotnet restore
    ```

3. **Configurar la base de datos:**

    Actualiza la cadena de conexión a la base de datos en el archivo `appsettings.json`:

    ```json
    {
      "ConnectionStrings": {
        "TrelloMySqlConn": "Server=127.0.0.1; User ID=root; Password=33286489; Database=Trello; AllowPublicKeyRetrieval=True;"
      }
    }
    ```

    Ejecuta las migraciones para configurar la base de datos:

    ```sh
    dotnet ef database update
    ```

4. **Ejecutar la aplicación:**

    ```sh
    dotnet run
    ```

    La API estará disponible en `http://localhost:5272`.

## Usage

Una vez que la API está en ejecución, puedes interactuar con ella utilizando herramientas como [Postman](https://www.postman.com/) o [Swagger](https://swagger.io/).

### Endpoints principales:

- **Tableros (Boards):**
  - `POST /api/v1/Board/create` - Crea un nuevo tablero.
  - `GET /api/v1/Board/get-columnscards/{boardId}` - Obtiene las columnas y tarjetas de un tablero por su ID.
  - `GET /api/v1/Board/permissions/{boardId}` - Verifica los permisos para un tablero por su ID.
  - `PUT /api/v1/Board/update/{id}` - Actualiza un tablero existente por su ID.
  - `DELETE /api/v1/Board/delete/{id}` - Elimina un tablero por su ID.
  - `DELETE /api/v1/Board/remove/members` - Elimina un miembro de un tablero.
  - `POST /api/v1/Board/add/members` - Agrega un miembro a un tablero.
  - `GET /api/v1/Board/{boardId}/members` - Obtiene los miembros de un tablero por su ID.
  - `GET /api/v1/Board/user/workspaces` - Obtiene los tableros asociados con los espacios de trabajo del usuario actual.
  - `POST /api/v1/Board/add/labels` - Agrega una etiqueta a un tablero.
  - `PUT /api/v1/Board/update/label/{id}` - Actualiza una etiqueta existente por su ID.
  - `DELETE /api/v1/Board/remove/labels` - Elimina una etiqueta de un tablero.
  - `GET /api/v1/Board/{boardId}/labels` - Obtiene las etiquetas de un tablero por su ID.

- **Tarjetas (Cards):**
  - `PUT /api/v1/Card/update-cards-order` - Actualiza el orden de las tarjetas.
  - `POST /api/v1/Card/create` - Crea una nueva tarjeta.
  - `POST /api/v1/Card/add-card-members` - Agrega miembros a una tarjeta.
  - `PUT /api/v1/Card/update` - Actualiza una tarjeta existente.
  - `PUT /api/v1/Card/move-between-columns` - Mueve una tarjeta entre columnas.
  - `DELETE /api/v1/Card/delete/{id}` - Elimina una tarjeta por su ID.
  - `DELETE /api/v1/Card/remove-card-member` - Elimina un miembro de una tarjeta.
  - `GET /api/v1/Card/get-all` - Recupera todas las tarjetas.
  - `POST /api/v1/Card/add-attachment` - Agrega un adjunto a una tarjeta.
  - `DELETE /api/v1/Card/delete-attachments/{attachmentId}` - Elimina un adjunto de una tarjeta por su ID.
  - `PUT /api/v1/Card/update-attachments` - Actualiza los adjuntos de una tarjeta.
  - `POST /api/v1/Card/create-checklistItem` - Crea un ítem de checklist en una tarjeta.
  - `DELETE /api/v1/Card/delete-checklistItem/{checklistItemId}` - Elimina un ítem de checklist por su ID.
  - `PUT /api/v1/Card/update-checklistItem` - Actualiza un ítem de checklist.
  - `POST /api/v1/Card/create-cardLabel` - Crea una etiqueta para una tarjeta.
  - `DELETE /api/v1/Card/delete-cardLabel/{cardId}/{labelId}` - Elimina una etiqueta de una tarjeta por sus IDs.
  - `PUT /api/v1/Card/update-cardLabel` - Actualiza una etiqueta de una tarjeta.
  - `GET /api/v1/Card/get-all-cardLabels` - Recupera todas las etiquetas de tarjetas.
  - `GET /api/v1/Card/get-card/{id}/details` - Recupera los detalles de una tarjeta por su ID.
  - `GET /api/v1/Card/get-card-assignees` - Recupera los asignados de una tarjeta.
  - `PUT /api/v1/Card/update-card-dates` - Actualiza las fechas de una tarjeta.
  - `PUT /api/v1/Card/update-card-priority` - Actualiza la prioridad de una tarjeta.
  - `PUT /api/v1/Card/update-card-description` - Actualiza la descripción de una tarjeta.
  - `PUT /api/v1/Card/update-card-reporter` - Actualiza el reviewer de una tarjeta.

- **Columnas (Columns):**
  - `PUT /api/v1/Column/update-column-order` - Actualiza el orden de las columnas.
  - `POST /api/v1/Column/create-column` - Crea una nueva columna.
  - `PUT /api/v1/Column/update-column` - Actualiza una columna existente.
  - `DELETE /api/v1/Column/delete-column/{id}` - Elimina una columna por su ID.
  - `GET /api/v1/Column/get-all` - Recupera todas las columnas.

- **Usuarios (Users):**
  - `POST /api/v1/User/create` - Crea un nuevo usuario.
  - `DELETE /api/v1/User/delete/{id}` - Elimina un usuario por su ID.
  - `PUT /api/v1/User/update` - Actualiza un usuario existente.
  - `PUT /api/v1/User/update-password` - Actualiza la contraseña del usuario actual.
  - `GET /api/v1/User/get-by-id/{userId}` - Recupera un usuario por su ID.
  - `GET /api/v1/User/get-by-email/{email}` - Recupera un usuario por su email.
  - `POST /api/v1/User/refresh-token` - Refresca el token de acceso usando el token de actualización proporcionado.
  - `POST /api/v1/User/login` - Autentica a un usuario.
  - `GET /api/v1/User/workspaces` - Recupera los espacios de trabajo asociados con el usuario actual.
  - `GET /api/v1/User/search` - Busca usuarios basándose en el texto de búsqueda proporcionado.
  - `GET /api/v1/User/image` - Recupera una imagen por su URL relativa.

- **Espacios de trabajo (Workspaces):**
  - `POST /api/v1/Workspace/create` - Crea un nuevo espacio de trabajo.
  - `PUT /api/v1/Workspace/update/{id}` - Actualiza un espacio de trabajo existente.
  - `POST /api/v1/Workspace/add-member` - Agrega un usuario existente a un espacio de trabajo.
  - `DELETE /api/v1/Workspace/remove/members` - Elimina un miembro de un espacio de trabajo.
  - `GET /api/v1/Workspace/{workspaceId}/members` - Recupera los miembros de un espacio de trabajo por su ID.
  - `GET /api/v1/Workspace/owned` - Recupera los espacios de trabajo propiedad del usuario actual.

## Contributing

¡Contribuciones son bienvenidas! Para contribuir a Boardify, sigue estos pasos:

1. **Fork el repositorio.**
2. **Crea una rama de característica (feature branch):**

    ```sh
    git checkout -b feature/nueva-caracteristica
    ```

3. **Realiza los cambios necesarios y haz commits:**

    ```sh
    git commit -m 'Agrega una nueva característica'
    ```

4. **Envía tus cambios al repositorio remoto:**

    ```sh
    git push origin feature/nueva-caracteristica
    ```

5. **Crea un Pull Request.**

## License

Boardify está licenciado bajo la licencia [MIT](https://choosealicense.com/licenses/mit/).
