# MoviesAPI
🚀 A simple Movies API built with ASP.NET Core and Microsoft SQL Server, providing full CRUD operations to manage movies and genres efficiently.

📺 **Watch this video for more details:** 

<!-- Resize image -->
<p align="center">
  <a href="https://i9.ytimg.com/vi_webp/KNYJgYDEMRA/mqdefault.webp?v=67f4486d&sqp=CJCf0b8G&rs=AOn4CLDZizD83FQJhFDrBSUIgEcAOiEvzA">
    <img src="https://img.youtube.com/vi/Js_S_Pcy950/0.jpg" alt="Watch the video" width="800" />
  </a>
</p>



## 📄 API Endpoints

### 🎬 Movies Endpoints

| **Method** | **Endpoint**           | **Description**                  |
|------------|------------------------|----------------------------------|
| **GET**    | `/api/movies`           | Get all movies                  |
| **GET**    | `/api/movies/{id}`      | Get a movie by ID               |
| **GET**    | `/api/movies/GetByGenreId`| Get movies by genre ID        |
| **POST**   | `/api/movies`           | Create a new movie              |
| **PUT**    | `/api/movies/{id}`      | Update an existing movie        |
| **DELETE** | `/api/movies/{id}`      | Delete a movie                  |

### 🎭 Genres Endpoints

| **Method** | **Endpoint**           | **Description**                 |
|------------|------------------------|---------------------------------|
| **GET**    | `/api/genres`          | Get all genres                  |
| **GET**    | `/api/genres/{id}`     | Get a genre by ID               |
| **POST**   | `/api/genres`          | Create a new genre              |
| **PUT**    | `/api/genres/{id}`     | Update an existing genre        |
| **DELETE** | `/api/genres/{id}`     | Delete a genre                  |

---
