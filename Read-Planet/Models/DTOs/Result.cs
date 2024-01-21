
//namespace Read_Planet.Models.DTOs
//{
//    public class Result<TValue> : Result where TValue : class
//    {
//        private readonly TValue? _data;

//        public Result(TValue? data, bool isSuccess, IEnumerable<Error> errors) : base(isSuccess, errors)
//        {
//            _data = data;
//        }

//        public TValue Data => _data!;
//    }

//    public class Result
//    {
//        protected Result(bool isSuccess, IEnumerable<Error> errors)
//        {
//            if (isSuccess && errors.Any())
//                throw new InvalidOperationException("cannot be successful with error");
//            if (!isSuccess && !errors.Any())
//                throw new InvalidOperationException("cannot be unsuccessful without error");

//            IsSuccess = isSuccess;
//            Errors = errors;
//            IsFailure = !isSuccess;
//        }

//        public bool IsSuccess { get; }
//        public bool IsFailure { get; }

//        public IEnumerable<Error> Errors { get; }

//        public static Result Success() => new(true, Error.None);

//        public static Result<TValue> Success<TValue>(TValue value) where TValue : class => new(value, true, Error.None);

//        public static Result Failure(IEnumerable<Error> errors) => new(false, errors);

//        public static Result<TValue> Failure<TValue>(IEnumerable<Error> errors) where TValue : class => new Result<TValue>(null, false, errors);
//    }
//}

