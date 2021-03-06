﻿using Microsoft.EntityFrameworkCore;
using System.Linq;
using TicketManagement.Domain.Event;
using TicketManagement.Infrastructure.Db.Configurations;
using TicketManagement.Infrastructure.EventDispatching;
using TicketManagement.Shared;

namespace TicketManagement.Infrastructure.Db
{
    public class TicketManagementDbContext : DbContext
    {
        public TicketManagementDbContext(DbContextOptions<TicketManagementDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new EventsListReadModelTypeConfiguration());
            builder.ApplyConfiguration(new EventDetailsReadModelTypeConfiguration());

            builder.ApplyConfiguration(new OrderTypeConfiguration());
            builder.ApplyConfiguration(new OrderReadModelTypeConfiguration());
            builder.ApplyConfiguration<Event>(new EventTypeConfiguration());
        }
    }

    public class UnitOfWork
    {
        private readonly TicketManagementDbContext _ticketManagementDbContext;
        private readonly InternalDomainEventDispatcher _internalDomainEventDispatcher;

        public UnitOfWork(TicketManagementDbContext kritosaurusDbContext, InternalDomainEventDispatcher internalDomainEventDispatcher)
        {
            _ticketManagementDbContext = kritosaurusDbContext;
            _internalDomainEventDispatcher = internalDomainEventDispatcher;
        }

        public void Save()
        {
            using (var transaction = _ticketManagementDbContext.Database.BeginTransaction())
            {
                var modifiedEntries = _ticketManagementDbContext.ChangeTracker.Entries<IHasDomainEvents>().ToList();
                _ticketManagementDbContext.SaveChanges();

                foreach (var entry in modifiedEntries)
                {
                    var events = entry.Entity.UncommittedChanges();
                    if (events.Any())
                    {
                        _internalDomainEventDispatcher.Dispatch(events, _ticketManagementDbContext);
                        entry.Entity.MarkChangesAsCommitted();
                    }
                }

                _ticketManagementDbContext.SaveChanges();
                transaction.Commit();
            }
        }
    }
}
