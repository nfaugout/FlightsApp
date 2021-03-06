﻿using Flights.Domain.Events;
using Newtonsoft.Json;
using RDD.Domain;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Domain.Models
{
	public class Flight : Entity
	{
		public override string Name
		{
			get
			{
				return String.Format("From {0} to {1}", Departure.Name, Destination.Name);
			}
			protected set { throw new NotImplementedException(); }
		}
		public Guid DepartureId { get; private set; }
		public Airport Departure { get; private set; }
		public Guid DestinationId { get; private set; }
		public Airport Destination { get; private set; }
		public Guid PlaneId { get; private set; }
		public Plane Plane { get; private set; }
		public DateTime? DepartedAt { get; internal set; }
		public DateTime? ArrivedAt { get; internal set; }

		public bool IsPlaneFlying => DepartedAt.HasValue && !ArrivedAt.HasValue;

		private Flight() { }

		public Flight(Airport departure, Airport destination, Plane plane, DateTime? departedAt, DateTime? arrivedAt)
			: this(Guid.NewGuid(), departure, destination, plane, departedAt, arrivedAt) { }

		public Flight(Guid id, Airport departure, Airport destination, Plane plane, DateTime? departedAt, DateTime? arrivedAt)
		{
			Id = id;
			Departure = departure;
			Destination = destination;
			Plane = plane;
			DepartedAt = departedAt;
			ArrivedAt = arrivedAt;
		}

		public void Reset(IEventDispatcher dispatcher)
		{
			new FlightReset(this, dispatcher);

			Plane.ResetLocation(dispatcher, Departure.Location);
		}
	}
}
