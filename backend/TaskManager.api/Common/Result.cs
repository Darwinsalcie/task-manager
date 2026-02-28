namespace TaskManager.api.Common
{
    //Si la operación no retorna algo usamos este
    public class Result
    {
        public bool IsSucces { get; }
        public bool IsFailure => !IsSucces;
        
        //No necesitamos agregar ni quitar nada, por eso usamos IEnmuerable,
        //así exponemos la minima abstraccion necesaria en otras capas y no acoplamos el consumirdor
        
        //Al usar IEnumerable pordemos construir el objeto con cualquier tipo de dato que implemnete esa interfaz
        //esto nos flexibiliza mucho para construir cualquier listas de errores con diferentes tipos de datos.
        public IEnumerable<string> Errors { get; }

        //Solo la clase y sus hijos pueden constuir la clase
        protected Result(bool isSucces, IEnumerable<string> errors)
        {
            IsSucces = isSucces;
            Errors = errors;
        }

        //Una clase estatico no nos permite crear objetos de ella
        //Con estos metodos estaticos obligamos a instanciar la clase
        //como está establecida en los metodos.
        public static Result Success()
            => new Result(true, Enumerable.Empty<string>());

        /// <summary>
        /// Recibe lista de errores y crea el objeto con los errores
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static Result Failure(IEnumerable<string> errors)
            => new Result(false, errors);

        /// <summary>
        /// Pasamos un error que puede ser harcodeado como
        /// "El usuario no existe"
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public static Result Failure(string error)
            => new Result(false, new[] { error });
    }

    //Heredamos para reutilizar la logica de Result y si la operación retorna un objeto por ejemplo
    //usamos este.
    public class Result<T>: Result
    {
        public T? Value { get; }

        private Result(bool isSucces, T? value, IEnumerable<string> errors)
            : base(isSucces, errors)
        {
            Value = value;
        }

        public static Result<T> Succes(T value)
            => new Result<T>(true, value, Enumerable.Empty<string>());
        
        /*Default behavior.
        En tiempo de compilación dependiendo si T es:
        *objeto => default = null,
        *numero => default = 0,
        *bool => default = false
         */
        //No queremos pasar nada, pero al ser generico usamos default.
        public static Result<T> Failure(IEnumerable<string> errors)
            => new Result<T>(false, default, errors);
        public static Result<T> Failure(string error)
            => new Result<T>(false, default, new[] { error });
    }
}
