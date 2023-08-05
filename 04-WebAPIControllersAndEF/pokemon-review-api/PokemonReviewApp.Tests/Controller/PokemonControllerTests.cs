using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Controllers;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonReviewApp.Tests.Controller;
public class PokemonControllerTests
{
    private readonly IPokemonRepository _pokemonRepository;
    private readonly IReviewRepository _reviewerRepository;
    private readonly IMapper _mapper;
    public PokemonControllerTests()
    {
        _pokemonRepository = A.Fake<IPokemonRepository>();
        _reviewerRepository = A.Fake<IReviewRepository>();
        _mapper = A.Fake<IMapper>();
    }

    [Fact]
    public void PokemonController_GetPokemons_ReturnOk()
    {
        //Arrange 
        /*
         Because the repository method returns ICollection: 
                ICollection<Pokemon> GetPokemons();
         */
        var pokemons = A.Fake<ICollection<PokemonDto>>();
        /*
         And because we are mapping the ICollection to List
         */
        var pokemonList = A.Fake<List<PokemonDto>>();
        //Mapping
        A.CallTo(() =>
        _mapper.Map<List<PokemonDto>>(pokemons)).Returns(pokemonList);

        var controller = new PokemonController
            (_pokemonRepository, _reviewerRepository,
            _mapper);

        //Act 
        var result = controller.GetPokemons();

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

    }

    [Fact]
    public void PokemController_CreatePokemon_ReturnOk()
    {
        //Arrange 
        int ownerId = 1;
        int catId = 2;
        var pokemon = A.Fake<Pokemon>();
        var pokemonCreate = A.Fake<PokemonDto>();
        var pokemons = A.Fake<ICollection<PokemonDto>>();
        var pokemonList = A.Fake<IList<PokemonDto>>();

        A.CallTo(() => _pokemonRepository.
        GetPokemonTrimToUpper(pokemonCreate)).Returns(pokemon);

        A.CallTo(() => _mapper.Map<Pokemon>(pokemonCreate))
            .Returns(pokemon);

        A.CallTo(() => _pokemonRepository.
        CreatePokemon(ownerId, catId, pokemon)).Returns(true);

        var controller = new PokemonController
            (_pokemonRepository, _reviewerRepository,
            _mapper);


        //Act
        var result = controller.
            CreatePokemon(ownerId,catId,pokemonCreate);
        
        //Assert
        result.Should().NotBeNull();
    }   

}
