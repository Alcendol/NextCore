public class IsReading{
    public required string IsReadingId {get; set;}
    public required int userId {get; set;}
    public required string eBookId {get; set;}
    public required int lastPage {get; set;} // jika statusnya notset, lastpage bisa kosong
    public required DateTime lastRead {get; set;} // Terakhir kali baca kapan (dihitung ketika status sudah ter-set)
    public required readStatus readStatus {get; set;} // bisa not set, not finished, finisihed, on progress (sifatnya optional)
}

public enum readStatus{
    Finished,
    OnProgress
}