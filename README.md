# Sales Date Prediction - Aplicación Fullstack

## Información para construir y ejecutar el proyecto

- El backend corre en **https** en el puerto **5001**.
- El frontend corre en el puerto **7200**.
- La aplicación usa una base de datos SQL Server local.
- La configuración de la conexión a la base de datos se encuentra en el archivo `appsettings.json` dentro de la carpeta `SalesDatePrediction.Api`.
- Se eliminó la carpeta `node_modules` para mantener el repositorio limpio, por lo que es necesario ejecutar `npm install` en la carpeta del frontend para instalar las dependencias.

### Pasos para ejecutar la aplicación

1. Clonar el repositorio.
2. Configurar la cadena de conexión a la base de datos en el archivo `SalesDatePrediction.Api/appsettings.json` según tu entorno local.
3. En la carpeta del backend (`SalesDatePrediction.Api`), restaurar paquetes y correr la API.
4. En la carpeta del frontend, ejecutar `npm install` para instalar dependencias.
5. Ejecutar el frontend con `npm start` o el comando que uses para levantar la aplicación Angular.
6. Asegurarse que el backend esté corriendo en https://localhost:5001 y el frontend en http://localhost:7200.

---

## Información relevante sobre la prueba técnica

- Esta prueba fue realizada en tiempos libres, ya que actualmente trabajo full-time.
- La aplicación no está 100% terminada por limitaciones de tiempo.
- No se implementaron pruebas unitarias ni de integración, aunque entiendo la importancia de estas.
- La arquitectura usada es una híbrida inspirada en Clean Architecture y principios SOLID, no es una implementación estricta debido al tiempo disponible.

---

## Explicación de cómo se ejecutó la prueba

- Se diseñó y desarrolló un backend en .NET Core con API REST, consumida por un frontend en Angular.
- Se trabajó con SQL Server local para la persistencia.
- Se implementaron funcionalidades clave para mostrar conocimiento en arquitectura, patrones y buenas prácticas.
- La intención es ampliar la aplicación y agregar pruebas si se dispone de más tiempo.

---

Gracias por la oportunidad y quedo atento a cualquier comentario o duda.

# Prueba tecnica completa

##Decidí completar esta prueba técnica a pesar de haber pasado la fecha inicial de entrega, ya que considero importante cerrar bien cada tarea que comienzo. No me gusta dejar las cosas a medias.

- ## ✅ Funcionalidades completadas

### Frontend
- Se implementa el buscador.
- Se desarrolla la funcionalidad para crear órdenes, dejándo la aplicación completamente funcional.

### Backend
- Se agregan las pruebas unitarias de la aplicacion.
- Se asegura la correcta integración con el frontend.

### Aplicación adicional: gráfico de barras con D3.js
- Se desarrolla la aplicación utilizando **vanilla JavaScript** y **D3.js**.
- Esta aplicación permite ingresar números y graficarlos como un gráfico de barras horizontal con colores alternados.
