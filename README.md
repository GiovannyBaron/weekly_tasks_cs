# weekly_tasks_cs
Aplicacion para crear y visualizar tareas de usuarios

# Carpetas
- DB: cuenta con el contexto para realizar la conexión de la base de datos SQL Server.
- Entities: contiene el modelo propio de .Net para el usuario (User).
- Models:
  - AuthUser: Modelos para registro y login del usuario a partir de JWT.
  - Data: Modelos para la creación y visualización de tareas semanales.
- Services: servicio e interfaz para la autenticación.
- UserTasksRepository: servicio e interfaz para el repositorio de tareas semanales de los usuarios.
