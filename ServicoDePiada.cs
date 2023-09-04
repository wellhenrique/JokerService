namespace App.WindowsService
{
  public sealed class ServicoDePiada
  {
    public string GetJoke()
    {
      Piada piada = _piadas.ElementAt(
          Random.Shared.Next(_piadas.Count));

      return $"{piada.Setup}{Environment.NewLine}{piada.Piunchline}";
    }

    // Piadas de programação retiradas de:
    // https://github.com/eklavyadev/karljoke/blob/main/source/jokes.json
    private readonly HashSet<Piada> _piadas = new()
        {
            new Piada("Qual é a melhor coisa sobre um Boolean?", "Mesmo se você estiver errado, você está apenas fora por um pouco."),
            new Piada("Qual é o jeito orientado a objetos de enriquecer?", "Herança"),
            new Piada("Por que o programador abandonou seu emprego?", "Porque ele não entendeu arrays."),
            new Piada("Por que os programadores sempre confundem o Halloween e o Natal?", "Porque Oct 31 == Dec 25"),
            new Piada("Quantos programadores são necessários para trocar uma lâmpada?", "Nenhum, isso é um problema de hardware"),
            new Piada("Se você colocar um milhão de macacos em um milhão de teclados, um deles eventualmente escreverá um programa Java", "o resto deles escreverá Perl"),
            new Piada("['hip', 'hip']", "(hip hip array)"),
            new Piada("Para entender o que é recursão...", "Você primeiro precisa entender o que é recursão"),
            new Piada("Existem 10 tipos de pessoas neste mundo...", "As que entendem binário e as que não entendem"),
            new Piada("Qual música uma exceção cantaria?", "Não pode me pegar - Avicii"),
            new Piada("Por que os programadores Java usam óculos?", "Porque eles não veem C#"),
            new Piada("Como você verifica se uma página da web é HTML5?", "Experimente no Internet Explorer"),
            new Piada("Uma interface do usuário é como uma piada.", "Se você precisa explicar, então não é tão boa."),
            new Piada("Eu ia contar uma piada sobre UDP...", "...mas você pode não entender."),
            new Piada("A punchline frequentemente chega antes da configuração.", "Você conhece o problema com piadas de UDP?"),
            new Piada("Por que os desenvolvedores de C# e Java continuam quebrando seus teclados?", "Porque eles usam uma linguagem fortemente tipada."),
            new Piada("Toc-toc.", "Uma condição de corrida. Quem está aí?"),
            new Piada("Qual é a melhor parte das piadas de TCP?", "Eu posso continuar contando até você entendê-las."),
            new Piada("Um programador coloca dois copos em sua mesa de cabeceira antes de dormir.", "Um cheio, caso ele tenha sede, e um vazio, caso ele não tenha."),
            new Piada("Existem 10 tipos de pessoas neste mundo.", "As que entendem binário, as que não entendem e as que não esperavam uma piada em base 3."),
            new Piada("O que o roteador disse ao médico?", "Isso dói quando IP."),
            new Piada("Um pacote IPv6 está saindo de casa.", "Ele não vai a lugar nenhum."),
            new Piada("Três declarações SQL entram em um bar NoSQL. Logo, elas saem", "Elas não conseguiram encontrar uma mesa.")
        };
  }

  readonly record struct Piada(string Setup, string Piunchline);
}
