# FilmLab.Backend
This application uses Onion Architecture and AutoMapper to promote good software design and architecture, and to facilitate mapping between objects in the application.
Onion Architecture is a software design pattern that helps to separate concerns in a software application, and to promote testability, maintainability, and scalability. The architecture consists of concentric layers, with the domain model at the center, and the infrastructure and UI layers on the outer layers. Each layer has its own responsibilities, and communication between layers is strictly defined and controlled.

WorkFlow
A user requests information about a particular movie or searches for a movie based on certain criteria.
The application retrieves the relevant movie data from the database, along with any associated Reviews.
The application presents the movie information to the user in a clear and concise manner, including a summary of reviews and ratings.
If the user wants to add a new Review, they can submit it through a user interface, triggering the creation of a new Review entity.
The application validates the Review and associates it with the relevant Movie entity.
The Review entity is saved to the database, allowing it to be retrieved and displayed in future movie searches or reviews.
