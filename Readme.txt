sql
1. Am creat baza de date

DAL
1. Instalez entity framework
2. Fac reverse engineering. Pe baza tabelelor se creeaza entitatile, maparea si contextul.
3. Creez un repository general pentru operatii CRUD 
4. Pentru fiecare entitate fac cate un repository derivat din rep general
5. Creez implementarile pentru fiecare repository
6. Creez  o clasa general care inglobeaza toate repository-urile 

BLL
1. Creez logica pentru fiecare entitate
2. Fiecare clasa are acces la DAL. Acesta este primit ca parametru in constructor
3. Dal-ul folosit va fi ales de catre unity, el are grija de dependency injection
4. Creez  o clasa general care inglobeaza toata logica

- am creat authLogic care se ocupa de locica de autentificare, inregistrare

metode:
->public int Validate(string username, string password)
verifica daca in baza de date exista un tuplu ce corespunde cu datele introduse

->public string Authenticate(string username, string password)
apeleaza validate. Daca userul si pass carespund se updateaza tokenul in baza de date si se intoarce stringul updatat

-> public string Register(string username, string password, string email)
verifica disponibilitatea numelui si introduce un nou user in baza de date

-> public bool VerifyTokenDate(string tokenString)
verifica tokenul din baza de date. Daca nu exista sau este expirat->eroare. Altfel se updateaza data de expirare si se intoarce ok


WebAPI
1. Am instalat Microsoft aspNET WebApi Cors
2. Am adaugat in webapiconfig.cs:
	config.Formatters.Remove(config.Formatters.XmlFormatter);
	config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));