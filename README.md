# CLINICA ODONTOLOGICA

## Este é um sistema simples de gerenciamento de clínica que permite o cadastro de pacientes e o agendamento de consultas.

---

## Funcionalidades

- **Cadastro de pacientes**
- **Exclusão de pacientes**
- **Listagem de pacientes** (ordenado por CPF ou nome)
- **Agendamento de consultas**
- **Cancelamento de consultas**
- **Listagem de consultas** (todas ou por período)

---

## Estrutura do Código

### Classes

- **Patient**: Representa um paciente com CPF, nome e data de nascimento.
- **Appointment**: Representa uma consulta com CPF do paciente, data, hora de início e hora de término.
- **Clinic**: Gerencia os pacientes e consultas, incluindo métodos para adicionar, remover, listar pacientes e agendar, cancelar, listar consultas.

### Métodos Principais

- **AddPatient(Patient patient)**: Adiciona um novo paciente.
- **RemovePatient(string cpf)**: Remove um paciente existente.
- **ScheduleAppointment(Appointment appointment)**: Agenda uma nova consulta.
- **CancelAppointment(string cpf, DateTime date, TimeSpan startTime)**: Cancela uma consulta existente.
- **ListPatients(bool orderByCPF)**: Lista todos os pacientes, ordenados por CPF ou nome.
- **ListAppointments(DateTime? startDate, DateTime? endDate)**: Lista todas as consultas ou consultas dentro de um período específico.

---

## Como Usar

1. **Execute o programa.**
2. **Use o menu principal** para navegar entre o cadastro de pacientes e a agenda.
3. **Siga as instruções no console** para adicionar, remover, listar pacientes e agendar, cancelar, listar consultas.

---

## Exemplo de Uso

### Cadastro de Paciente
- Selecione **"1-Cadastro de pacientes"** no menu principal.
- Siga as instruções para inserir CPF, nome e data de nascimento do paciente.

### Agendamento de Consulta
- Selecione **"2-Agenda"** no menu principal.
- Siga as instruções para inserir CPF do paciente, data, hora de início e hora de término da consulta.

### Listagem de Pacientes
- Selecione **"1-Cadastro de pacientes"** no menu principal.
- Escolha listar pacientes ordenados por CPF ou nome.

### Listagem de Consultas
- Selecione **"2-Agenda"** no menu principal.
- Escolha listar todas as consultas ou consultas dentro de um período específico.

---

## Requisitos
- **.NET Core SDK**

---

## Compilação e Execução

Para compilar e executar o programa, use os seguintes comandos no terminal:

```bash
# Para compilar o projeto
$ dotnet build

# Para executar o projeto
$ dotnet run
```

---

## Contribuição

Sinta-se à vontade para contribuir com melhorias ou correções. Faça um fork do repositório, crie uma branch para suas alterações e envie um pull request.

---

## Licença

Este projeto está licenciado sob a licença **MIT**. Consulte o arquivo LICENSE para obter mais informações.

