using bigsmollAPI.DTO;
using littlebigsmoll;
using Microsoft.AspNetCore.Mvc;

namespace bigsmollAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Kybiki : ControllerBase
    {
        private Random random;
        private int dice1;
        private int dice2;
        private readonly User11Context _context;

        public Kybiki(User11Context context)
        {
            _context = context;
        }

        [HttpPut("Classic")]
        public ActionResult<UserDTO> ClassicRoulette(int guess , decimal stake, UserDTO user)
        {
            var data = _context.Users.Find(user.Id);
            if (data==null)
            {
                return BadRequest("Иди регайся");
            }
            if (data.Balance < stake)
            {
                return BadRequest("Иди работай");
            }
            data.Balance = data.Balance - stake;
            user.Win = false;
            Random random = new Random();
            //int number = random.Next(2, 13);
            dice1 = random.Next(1, 7); // Генерируем число от 1 до 6 для первого кубика
            dice2 = random.Next(1, 7); // Генерируем число от 1 до 6 для второго кубика
            int sum = dice1 + dice2;// Генерируем случайное число от  1 до  100

            if (guess == sum)
            {
                user.Win = true;
                data.Balance = data.Balance + stake * 1.2m;
            }
            else 
            {

                user.Win = false;
                data.Balance = data.Balance - stake * 1.2m;

            }


            //else if (number > 99 && color == "Green")
            //{
            //    user.Win = true;
            //    user.Balance = user.Balance + stake * 2;
            //}

            user.Balance = data.Balance;
            _context.SaveChanges();
            return Ok(user);
        }

        //[HttpPut("Wheel")]
        //public ActionResult<UserDTO> WheelRoulette(decimal stake, UserDTO user)
        //{
        //    var data = _context.Users.Find(user.Id);
        //    user.Balance = data.Balance - stake;
        //    Random random = new Random();
        //    int number = random.Next(1, 17);

        //    if (number <= 6)
        //    {
        //        user.Balance = user.Balance + stake * 0.5m;
        //    }
        //    else if (number > 6 && number <= 10)
        //    {
        //        user.Balance = user.Balance + stake * 1.2m;
        //    }
        //    else if (number > 10 && number <= 14)
        //    {

        //    }
        //    else if (number == 15)
        //    {
        //        user.Balance = user.Balance - stake * 2;
        //    }
        //    else if (number == 16)
        //    {
        //        user.Balance = user.Balance + stake * 3;
        //    }
        //    data.Balance = user.Balance;
        //    _context.SaveChanges();
        //    return Ok(user);
        //}


    }
}
