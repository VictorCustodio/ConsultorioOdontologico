using System;
using System.Collections.Generic;
using System.Linq;

public class Patient
{
	public string CPF { get; set; }
	public string Name { get; set; }
	public DateTime BirthDate { get; set; }
}

public class Appointment
{
	public string PatientCPF { get; set; }
	public DateTime Date { get; set; }
	public TimeSpan StartTime { get; set; }
	public TimeSpan EndTime { get; set; }
}

public class Clinic
{
	private List<Patient> patients = new List<Patient>();
	private List<Appointment> appointments = new List<Appointment>();

	public void AddPatient(Patient patient)
	{
		if (!IsValidCPF(patient.CPF))
		{
			Console.WriteLine("Erro: CPF inválido.");
			return;
		}
		if (patient.Name.Length < 5)
		{
			Console.WriteLine("Erro: Nome deve ter pelo menos 5 caracteres.");
			return;
		}
		if (patients.Any(p => p.CPF == patient.CPF))
		{
			Console.WriteLine("Erro: CPF já cadastrado.");
			return;
		}
		if ((DateTime.Now - patient.BirthDate).TotalDays / 365 < 13)
		{
			Console.WriteLine("Erro: paciente deve ter pelo menos 13 anos.");
			return;
		}
		patients.Add(patient);
		Console.WriteLine("Paciente cadastrado com sucesso!");
	}

	public void RemovePatient(string cpf)
	{
		var patient = patients.FirstOrDefault(p => p.CPF == cpf);
		if (patient == null)
		{
			Console.WriteLine("Erro: paciente não cadastrado.");
			return;
		}
		if (appointments.Any(a => a.PatientCPF == cpf && a.Date >= DateTime.Now))
		{
			Console.WriteLine("Erro: paciente está agendado.");
			return;
		}
		patients.Remove(patient);
		appointments.RemoveAll(a => a.PatientCPF == cpf);
		Console.WriteLine("Paciente excluído com sucesso!");
	}

	public void ScheduleAppointment(Appointment appointment)
	{
		if (!patients.Any(p => p.CPF == appointment.PatientCPF))
		{
			Console.WriteLine("Erro: paciente não cadastrado.");
			return;
		}
		if (appointment.Date < DateTime.Now.Date || (appointment.Date == DateTime.Now.Date && appointment.StartTime <= DateTime.Now.TimeOfDay))
		{
			Console.WriteLine("Erro: agendamento deve ser para um período futuro.");
			return;
		}
		if (appointment.EndTime <= appointment.StartTime)
		{
			Console.WriteLine("Erro: Hora final deve ser maior que a hora inicial.");
			return;
		}
		if (appointments.Any(a => a.PatientCPF == appointment.PatientCPF && a.Date >= DateTime.Now))
		{
			Console.WriteLine("Erro: paciente já possui um agendamento futuro.");
			return;
		}
		if (appointments.Any(a => a.Date == appointment.Date && ((a.StartTime <= appointment.StartTime && a.EndTime > appointment.StartTime) || (a.StartTime < appointment.EndTime && a.EndTime >= appointment.EndTime))))
		{
			Console.WriteLine("Erro: já existe uma consulta agendada nesse horário.");
			return;
		}
		appointments.Add(appointment);
		Console.WriteLine("Agendamento realizado com sucesso!");
	}

	public void CancelAppointment(string cpf, DateTime date, TimeSpan startTime)
	{
		var appointment = appointments.FirstOrDefault(a => a.PatientCPF == cpf && a.Date == date && a.StartTime == startTime);
		if (appointment == null)
		{
			Console.WriteLine("Erro: agendamento não encontrado.");
			return;
		}
		if (appointment.Date < DateTime.Now.Date || (appointment.Date == DateTime.Now.Date && appointment.StartTime <= DateTime.Now.TimeOfDay))
		{
			Console.WriteLine("Erro: cancelamento só pode ser realizado se for de um agendamento futuro.");
			return;
		}
		appointments.Remove(appointment);
		Console.WriteLine("Agendamento cancelado com sucesso!");
	}

	public void ListPatients(bool orderByCPF)
	{
		var orderedPatients = orderByCPF ? patients.OrderBy(p => p.CPF) : patients.OrderBy(p => p.Name);
		Console.WriteLine("------------------------------------------------------------");
		Console.WriteLine("CPF Nome Dt.Nasc. Idade");
		Console.WriteLine("------------------------------------------------------------");
		foreach (var patient in orderedPatients)
		{
			Console.WriteLine($"{patient.CPF} {patient.Name} {patient.BirthDate:dd/MM/yyyy} {(DateTime.Now.Year - patient.BirthDate.Year)}");
			var futureAppointment = appointments.FirstOrDefault(a => a.PatientCPF == patient.CPF && a.Date >= DateTime.Now);
			if (futureAppointment != null)
			{
				Console.WriteLine($"Agendado para: {futureAppointment.Date:dd/MM/yyyy}");
				Console.WriteLine($"{futureAppointment.StartTime:hh\\:mm} às {futureAppointment.EndTime:hh\\:mm}");
			}
		}
		Console.WriteLine("------------------------------------------------------------");
	}

	public void ListAppointments(DateTime? startDate, DateTime? endDate)
	{
		var filteredAppointments = appointments.Where(a => (!startDate.HasValue || a.Date >= startDate) && (!endDate.HasValue || a.Date <= endDate)).OrderBy(a => a.Date).ThenBy(a => a.StartTime);
		Console.WriteLine("-------------------------------------------------------------");
		Console.WriteLine("Data H.Ini H.Fim Tempo Nome Dt.Nasc.");
		Console.WriteLine("-------------------------------------------------------------");
		foreach (var appointment in filteredAppointments)
		{
			var patient = patients.First(p => p.CPF == appointment.PatientCPF);
			Console.WriteLine($"{appointment.Date:dd/MM/yyyy} {appointment.StartTime:hh\\:mm} {appointment.EndTime:hh\\:mm} {(appointment.EndTime - appointment.StartTime):hh\\:mm} {patient.Name} {patient.BirthDate:dd/MM/yyyy}");
		}
		Console.WriteLine("-------------------------------------------------------------");
	}

	private bool IsValidCPF(string cpf)
	{
		// Implement CPF validation logic here
		return true;
	}
}

class Program
{
	static void Main()
	{
		Clinic clinic = new Clinic();
		while (true)
		{
			Console.WriteLine("Menu Principal");
			Console.WriteLine("1-Cadastro de pacientes");
			Console.WriteLine("2-Agenda");
			Console.WriteLine("3-Fim");
			var choice = Console.ReadLine();
			if (choice == "1")
			{
				ManagePatients(clinic);
			}
			else if (choice == "2")
			{
				ManageAppointments(clinic);
			}
			else if (choice == "3")
			{
				break;
			}
		}
	}

	static void ManagePatients(Clinic clinic)
	{
		while (true)
		{
			Console.WriteLine("Menu do Cadastro de Pacientes");
			Console.WriteLine("1-Cadastrar novo paciente");
			Console.WriteLine("2-Excluir paciente");
			Console.WriteLine("3-Listar pacientes (ordenado por CPF)");
			Console.WriteLine("4-Listar pacientes (ordenado por nome)");
			Console.WriteLine("5-Voltar p/ menu principal");
			var choice = Console.ReadLine();
			if (choice == "1")
			{
				Console.Write("CPF: ");
				var cpf = Console.ReadLine();
				Console.Write("Nome: ");
				var name = Console.ReadLine();
				Console.Write("Data de nascimento (DDMMAAAA): ");
				var birthDate = DateTime.ParseExact(Console.ReadLine(), "ddMMyyyy", null);
				clinic.AddPatient(new Patient { CPF = cpf, Name = name, BirthDate = birthDate });
			}
			else if (choice == "2")
			{
				Console.Write("CPF: ");
				var cpf = Console.ReadLine();
				clinic.RemovePatient(cpf);
			}
			else if (choice == "3")
			{
				clinic.ListPatients(true);
			}
			else if (choice == "4")
			{
				clinic.ListPatients(false);
			}
			else if (choice == "5")
			{
				break;
			}
		}
	}

	static void ManageAppointments(Clinic clinic)
	{
		while (true)
		{
			Console.WriteLine("Agenda");
			Console.WriteLine("1-Agendar consulta");
			Console.WriteLine("2-Cancelar agendamento");
			Console.WriteLine("3-Listar agenda");
			Console.WriteLine("4-Voltar p/ menu principal");
			var choice = Console.ReadLine();
			if (choice == "1")
			{
				Console.Write("CPF: ");
				var cpf = Console.ReadLine();
				Console.Write("Data da consulta (DDMMAAAA): ");
				var date = DateTime.ParseExact(Console.ReadLine(), "ddMMyyyy", null);
				Console.Write("Hora inicial (HHMM): ");
				var startTime = TimeSpan.ParseExact(Console.ReadLine(), "hhmm", null);
				Console.Write("Hora final (HHMM): ");
				var endTime = TimeSpan.ParseExact(Console.ReadLine(), "hhmm", null);
				clinic.ScheduleAppointment(new Appointment { PatientCPF = cpf, Date = date, StartTime = startTime, EndTime = endTime });
			}
			else if (choice == "2")
			{
				Console.Write("CPF: ");
				var cpf = Console.ReadLine();
				Console.Write("Data da consulta (DDMMAAAA): ");
				var date = DateTime.ParseExact(Console.ReadLine(), "ddMMyyyy", null);
				Console.Write("Hora inicial (HHMM): ");
				var startTime = TimeSpan.ParseExact(Console.ReadLine(), "hhmm", null);
				clinic.CancelAppointment(cpf, date, startTime);
			}
			else if (choice == "3")
			{
				Console.Write("Apresentar a agenda T-Toda ou P-Periodo: ");
				var periodChoice = Console.ReadLine();
				if (periodChoice.ToUpper() == "T")
				{
					clinic.ListAppointments(null, null);
				}
				else if (periodChoice.ToUpper() == "P")
				{
					Console.Write("Data inicial (DDMMAAAA): ");
					var startDate = DateTime.ParseExact(Console.ReadLine(), "ddMMyyyy", null);
					Console.Write("Data final (DDMMAAAA): ");
					var endDate = DateTime.ParseExact(Console.ReadLine(), "ddMMyyyy", null);
					clinic.ListAppointments(startDate, endDate);
				}
			}
			else if (choice == "4")
			{
				break;
			}
		}
	}
}
