﻿using TargetsMicroservice.Models;
using TargetsMicroservice.Repositories.Interfaces;
using TargetsMicroservice.Requests;
using TargetsMicroservice.Responses;
using TargetsMicroservice.Services.Interfaces;

namespace TargetsMicroservice.Services.Implementations
{
    public class FlightsService : IFlightsService
    {
        private readonly IFlightsRepository _flightRepository;

        public FlightsService(IFlightsRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public async Task<Flight> CreateFlight(FlightBeginRequest request)
        {
            return await _flightRepository.CreateFlight(request);
        }

        public async Task<Flight> EndFlight(FlightEndRequest request)
        {
            var flight = GetFlightById(request.FlightID);
            if (flight == null) 
                return null;
            return await _flightRepository.EndFlight(flight.Result, request.EndTime);
        }

        public async Task<FlightResponse> GetFlightResponseById(long flightID)
        {
            return await _flightRepository.GetFlightResponseById(flightID);
        }

        public async Task<List<FlightResponse>> GetFlights()
        {
            return await _flightRepository.GetFlights();
        }

        public async Task<Flight> GetFlightById(long flightID)
        {
            return await _flightRepository.GetFlightById(flightID);
        }
    }
}
