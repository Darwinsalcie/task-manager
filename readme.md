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
5. La quinta desición a nivel de backend fue que tenemos que manejar todos los errores por el Result Pattern, pero los dattanotations no entran en el Result Pattern, para no hacer coass raras decií que para el ambito de este proyecto y lo que conlleva podemos dejarlo así aunque rompamos un poco el proposito de usar Result Pattern, ya cuando haga algo mas avanzado habrán mejores formas de centralizar el manejo de errores como al usar fluenvalidation.

**Capturas del Proyecto:**
**Main Layout**
<img width="1866" height="856" alt="image" src="https://github.com/user-attachments/assets/aed0cbf4-7636-486e-b764-81fbd041328b" />
**Edit Entity**
<img width="1842" height="856" alt="image" src="https://github.com/user-attachments/assets/244677a6-8b44-4d4a-876a-720793739de1" />
**New Task:**
<img width="1834" height="873" alt="image" src="https://github.com/user-attachments/assets/9cbf5e0f-d430-46e1-b165-4d4bb638cbb1" />
**Login:**
<img width="1834" height="860" alt="image" src="https://github.com/user-attachments/assets/7ffcf6d0-8e0f-43d9-af34-fbb817701b55" />
**Register:**
<img width="1824" height="868" alt="image" src="https://github.com/user-attachments/assets/443dc66e-2950-423a-bf4f-3c033eba7675" />



