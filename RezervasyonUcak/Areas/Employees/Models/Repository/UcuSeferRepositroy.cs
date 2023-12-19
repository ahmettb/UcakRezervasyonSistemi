namespace RezervasyonUcak.Areas.Employees.Models.Repository
{
	public class UcuSeferRepositroy : IUcusSeferRepository
	{
		private readonly AppDbContext dbContext;
		public UcuSeferRepositroy(AppDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public void addUcusSefer(UcusSefer sefer)
		{
			throw new NotImplementedException();
		}

		public void deleteUcusSefer(UcusSefer sefer)
		{
			throw new NotImplementedException();
		}

        public List<UcusSefer> getAllUcusSefer()
        {
            throw new NotImplementedException();
        }

        public List<UcusKonum> getAllUcusSeferKonum()
		{


List<UcusKonum> query=	dbContext.UcusKonum.ToList();

	
			return query;
		}

		public void getUcusSefer(UcusSefer sefer)
		{
			
			List<UcusSefer>seferListele=new List<UcusSefer>();
			//seferListele = dbContext.UcusSefers.Where(ucus => ucus.BaslangicKonum == sefer.BaslangicKonum && ucus.VarisKonum == sefer.VarisKonum);


		}

		public void updateUcusSefer(UcusSefer sefer)
		{
			throw new NotImplementedException();
		}
	}
}
