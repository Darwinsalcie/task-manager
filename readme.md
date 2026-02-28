Este es un repositorio en el que estaré desarrolando una web app Todo.
Está app trataré de hacerla sencilla, tratando de hacerlo de la mejor manera posible dentro de lo que sea razonable para la embergadura del proyecto.

Acá estaré docuemntando el por que de algunas decisiones de diseño.

1. Voy a utilizar un repositorio generico para manejar toda esa logica de acceso a datos que sea comun a las entidades.
2. Durante el desarrollo de este proyecto estuve estudiando el patron unit of work y evalué la posibilidad de utilizarlo para separar
   las responsabilidad de trackear los cambios en el repositorio generico y dejar alguna logica que se vaya a aplicar antes de guardar
   la entidad en la capa de servicio, pero decidí que usar unit of work no aportaría valor para lo que este proyecto implica.
3. Una de las decisiones que tomé fue dejar todo la logica del acceso a datos en el patron repositorio y manejar exclusivamente la lo logica
   de negocios en el la capa de servicios.
4. Como deseo manejar de manera clara y precisa los errores he decidido usar Result Object Pattern y como lo usaré en todo el proyecto
   voy a ponerlo en la ruta common para reducir el nivel de acoplamiento entre las capas de mi proyecto y no romper la direccion de dependencias.