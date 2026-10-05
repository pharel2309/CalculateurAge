namespace CalculateurAge.ViewModels;

public class CalculateurViewModel : BaseViewModel
{
	private const int AgeMajorite = 18;

	private string _nom = "";
	private DateTime _dateNaissance = DateTime.Today.AddYears(-20);
	private string _resultat = "";
	private bool _resultatVisibke;

	private int _age;
	private int _joursAvantAnniversaire;
	private string _message = "";
	private string _erreur = "";
	private bool _erreurVisible;

	public string Nom
	{
		get => _nom;
		set
		{
			if (SetField(ref _nom, value)) CalculerCommand.Rafraichir();
		}
	}

	public DateTime DateNaissance
	{
		get => _dateNaissance;
		set
		{
			if (value > DateTime.Today)
			{
				Message = "Date future non autorisée";
				return;
			}
			if (SetField(ref _dateNaissance, value))
			{
				UpdateAgeDependentProperties();
			}
		}
	}

	public String Resultat
	{
		get => _resultat;
		set => SetField(ref _resultat, value);
	}

	public bool ResultatVisible
	{
		get => _resultatVisibke;
		set => SetField(ref _resultatVisibke, value);
	}


	public int Age
	{
		get => _age;
		set => SetField(ref _age, value);
	}

	public int JoursAvantAnniversaire
	{
		get => _joursAvantAnniversaire;
		set => SetField(ref _joursAvantAnniversaire, value);
	}

	public string Message
	{
		get => _message;
		set => SetField(ref _message, value);
	}

	public string Erreur
	{
		get => _erreur;
		set => SetField(ref _erreur, value);
	}

	public bool ErreurVisible
	{
		get => _erreurVisible;
		set => SetField(ref _erreurVisible, value);
	}

	public DateTime DateMax => DateTime.Today;

	public RelayCommand CalculerCommand { get; }
	public RelayCommand EffacerCommand { get; }

	public CalculateurViewModel()
	{
		CalculerCommand = new RelayCommand(
			Calculer,
			() => !string.IsNullOrWhiteSpace(Nom));
		EffacerCommand = new RelayCommand(Effacer);
	}

	private void Calculer()
	{
		if (DateNaissance.Date > DateTime.Today)
		{
			Erreur = "La date de naissance ne peut pas être dans le futur.";
			ErreurVisible = true;
			return;
		}

		Erreur = "";
		ErreurVisible = false;
		UpdateAgeDependentProperties();
		int age = Age;
		Resultat = $"{Nom}, vous avez {age} ans.";
		ResultatVisible = true;
	}

	private void Effacer()
	{
		Age = 0;
		JoursAvantAnniversaire = 0;
		Message = "";
		Resultat = "";
		ResultatVisible = false;
		Erreur = "";
		ErreurVisible = false;
		CalculerCommand.Rafraichir();
	}

	private void UpdateAgeDependentProperties()
	{
		int age = DateTime.Today.Year - DateNaissance.Year;
		if (DateNaissance.Date > DateTime.Today.AddYears(-age)) age--;

		Age = age;
		Message = age >= AgeMajorite ? "majeur" : "mineur";
		JoursAvantAnniversaire = CalculerJoursAvantAnniversaire();
	}



	private int CalculerJoursAvantAnniversaire()
	{
		DateTime aujourdhui = DateTime.Today;
		DateTime anniversaire = GetAnniversaire(aujourdhui.Year);

		if (anniversaire < aujourdhui)
			anniversaire = GetAnniversaire(aujourdhui.Year + 1);

		return (anniversaire - aujourdhui).Days;
	}

	private DateTime GetAnniversaire(int annee)
		=> DateNaissance.AddYears(annee - DateNaissance.Year);
}
