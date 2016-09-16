//
// Role.cs
//
// Copyright (c) 2016 Takashi OTSUKI
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
//

using System.Collections.Generic;

namespace AIWolf.Lib
{
    /// <summary>
    /// Enumeration type for role.
    /// </summary>
    public enum Role
    {
        /// <summary>
        /// Uncertain.
        /// </summary>
        UNC,

        /// <summary>
        /// Bodyguard.
        /// </summary>
        BODYGUARD,

        /// <summary>
        /// Freemason. This is not used for now.
        /// </summary>
        FREEMASON,

        /// <summary>
        /// Medium.
        /// </summary>
        MEDIUM,

        /// <summary>
        /// Possessed human.
        /// </summary>
        POSSESSED,

        /// <summary>
        /// Seer.
        /// </summary>
        SEER,

        /// <summary>
        /// Villager.
        /// </summary>
        VILLAGER,

        /// <summary>
        /// Werewolf.
        /// </summary>
        WEREWOLF
    }

    /// <summary>
    /// Defines extension method of enum Role.
    /// </summary>
    public static class RoleExtensions
    {
        static Dictionary<Role, Species> roleSpeciesMap = new Dictionary<Role, Species>();

        static RoleExtensions()
        {
            roleSpeciesMap[Role.UNC] = Species.UNC;
            roleSpeciesMap[Role.BODYGUARD] = Species.HUMAN;
            roleSpeciesMap[Role.FREEMASON] = Species.HUMAN; // This is not used for now.
            roleSpeciesMap[Role.MEDIUM] = Species.HUMAN;
            roleSpeciesMap[Role.POSSESSED] = Species.HUMAN;
            roleSpeciesMap[Role.SEER] = Species.HUMAN;
            roleSpeciesMap[Role.VILLAGER] = Species.HUMAN;
            roleSpeciesMap[Role.WEREWOLF] = Species.WEREWOLF;
        }

        /// <summary>
        /// Returns the species the role belongs to.
        /// </summary>
        /// <param name="role">Role.</param>
        /// <returns>The species the role belongs to.</returns>
        public static Species GetSpecies(this Role role)
        {
            return roleSpeciesMap[role];
        }
    }
}
