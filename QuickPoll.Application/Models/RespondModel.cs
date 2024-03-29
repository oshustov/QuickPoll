﻿using Mapster;
using QuickPoll.Application.Entities;
using QuickPoll.Application.Interfaces;
using QuickPoll.Application.Polls.Commands;
using QuickPoll.Domain.Entities;

namespace QuickPoll.Application.Models;

public class RespondModel : BaseModel<RespondModel, Respond>
{
  public string PollId { get; set; }
  public long OptionId { get; set; }

  protected override void AddCustomRules()
  {
    Rules().Ignore(d => d.PollId)
      .AfterMapping(async (s, d) =>
      {
        var (_, id) = await MapContext.Current.GetService<IObfuscationService>().TryDeObfuscate(s.PollId);
        d.PollId = id;
      });
  }
}