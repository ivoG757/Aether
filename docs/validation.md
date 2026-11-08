# Validation

Shared validation constraints are stored in Aether.GCommon so that the same constraints can be reused throughout the project.

Validation is applied at multiple levels:

- Web view models use DataAnnotations for MVC model validation and client-side feedback.
- Service request DTOs use DataAnnotations and are explicitly evaluated through AttributeValidator. This ensures that service methods remain protected even when called outside an MVC controller.
- Domain models use attributes and EF Core configuration to define database column constraints.
- Business rules that require application state to remain inside the service layer.

Database foreign keys, unique indexes and length constraints provide the final layer integrity.
