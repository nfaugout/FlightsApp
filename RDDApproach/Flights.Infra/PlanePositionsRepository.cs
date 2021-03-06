﻿using Flights.Domain;
using Flights.Domain.Events;
using Flights.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Infra
{
    public class PlanePositionsRepository
	{
		public void ProcessEvent(Event @event)
		{
			switch(@event.Type)
			{
				case PlanePosition.EVENT_POSITION_CREATED:
					{
						var position = (PlanePosition)@event.Subject;

						Add(position);

						break;
					}
				default:
					throw new NotImplementedException();
			}
		}

		private void Add(PlanePosition position)
		{
			using (var context = new FlightsContext())
			{
				context.Entry(position).State = EntityState.Added;

				context.SaveChanges();
			}
		}
	}
}
