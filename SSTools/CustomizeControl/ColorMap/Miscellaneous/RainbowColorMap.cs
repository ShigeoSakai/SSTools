﻿using System.Drawing;
namespace SSTools.ColorMap.Miscellaneous
{
	/// <summary>
	/// Rainbowカラーマップ
	/// </summary>
	public class RainbowColorMap : ColorMapClass
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public RainbowColorMap()
		{
			colorMap = rainbow_map_;
		}
		/// <summary>
		/// カラーマップテーブル
		/// </summary>
		protected Color[] rainbow_map_ =
		{
			Color.FromArgb(127,0,255),		// 0
			Color.FromArgb(125,3,254),		// 1
			Color.FromArgb(123,6,254),		// 2
			Color.FromArgb(121,9,254),		// 3
			Color.FromArgb(119,12,254),		// 4
			Color.FromArgb(117,15,254),		// 5
			Color.FromArgb(115,18,254),		// 6
			Color.FromArgb(113,21,254),		// 7
			Color.FromArgb(111,25,254),		// 8
			Color.FromArgb(109,28,254),		// 9
			Color.FromArgb(107,31,254),		// 10
			Color.FromArgb(105,34,254),		// 11
			Color.FromArgb(103,37,254),		// 12
			Color.FromArgb(101,40,254),		// 13
			Color.FromArgb(99,43,254),		// 14
			Color.FromArgb(97,46,253),		// 15
			Color.FromArgb(95,49,253),		// 16
			Color.FromArgb(93,53,253),		// 17
			Color.FromArgb(91,56,253),		// 18
			Color.FromArgb(89,59,253),		// 19
			Color.FromArgb(87,62,253),		// 20
			Color.FromArgb(85,65,252),		// 21
			Color.FromArgb(83,68,252),		// 22
			Color.FromArgb(81,71,252),		// 23
			Color.FromArgb(79,74,252),		// 24
			Color.FromArgb(77,77,251),		// 25
			Color.FromArgb(75,80,251),		// 26
			Color.FromArgb(73,83,251),		// 27
			Color.FromArgb(71,86,251),		// 28
			Color.FromArgb(69,89,250),		// 29
			Color.FromArgb(67,92,250),		// 30
			Color.FromArgb(65,95,250),		// 31
			Color.FromArgb(63,97,250),		// 32
			Color.FromArgb(61,100,249),		// 33
			Color.FromArgb(59,103,249),		// 34
			Color.FromArgb(57,106,249),		// 35
			Color.FromArgb(55,109,248),		// 36
			Color.FromArgb(53,112,248),		// 37
			Color.FromArgb(51,115,248),		// 38
			Color.FromArgb(49,117,247),		// 39
			Color.FromArgb(47,120,247),		// 40
			Color.FromArgb(45,123,246),		// 41
			Color.FromArgb(43,126,246),		// 42
			Color.FromArgb(41,128,246),		// 43
			Color.FromArgb(39,131,245),		// 44
			Color.FromArgb(37,134,245),		// 45
			Color.FromArgb(35,136,244),		// 46
			Color.FromArgb(33,139,244),		// 47
			Color.FromArgb(31,142,243),		// 48
			Color.FromArgb(29,144,243),		// 49
			Color.FromArgb(27,147,243),		// 50
			Color.FromArgb(25,149,242),		// 51
			Color.FromArgb(23,152,242),		// 52
			Color.FromArgb(21,154,241),		// 53
			Color.FromArgb(19,157,241),		// 54
			Color.FromArgb(17,159,240),		// 55
			Color.FromArgb(15,162,239),		// 56
			Color.FromArgb(13,164,239),		// 57
			Color.FromArgb(11,167,238),		// 58
			Color.FromArgb(9,169,238),		// 59
			Color.FromArgb(7,171,237),		// 60
			Color.FromArgb(5,174,237),		// 61
			Color.FromArgb(3,176,236),		// 62
			Color.FromArgb(1,178,236),		// 63
			Color.FromArgb(0,180,235),		// 64
			Color.FromArgb(2,183,234),		// 65
			Color.FromArgb(4,185,234),		// 66
			Color.FromArgb(6,187,233),		// 67
			Color.FromArgb(8,189,232),		// 68
			Color.FromArgb(10,191,232),		// 69
			Color.FromArgb(12,193,231),		// 70
			Color.FromArgb(14,195,230),		// 71
			Color.FromArgb(16,197,230),		// 72
			Color.FromArgb(18,199,229),		// 73
			Color.FromArgb(20,201,228),		// 74
			Color.FromArgb(22,203,228),		// 75
			Color.FromArgb(24,205,227),		// 76
			Color.FromArgb(26,207,226),		// 77
			Color.FromArgb(28,209,226),		// 78
			Color.FromArgb(30,210,225),		// 79
			Color.FromArgb(32,212,224),		// 80
			Color.FromArgb(34,214,223),		// 81
			Color.FromArgb(36,215,223),		// 82
			Color.FromArgb(38,217,222),		// 83
			Color.FromArgb(40,219,221),		// 84
			Color.FromArgb(42,220,220),		// 85
			Color.FromArgb(44,222,220),		// 86
			Color.FromArgb(46,223,219),		// 87
			Color.FromArgb(48,225,218),		// 88
			Color.FromArgb(50,226,217),		// 89
			Color.FromArgb(52,228,216),		// 90
			Color.FromArgb(54,229,215),		// 91
			Color.FromArgb(56,230,215),		// 92
			Color.FromArgb(58,232,214),		// 93
			Color.FromArgb(60,233,213),		// 94
			Color.FromArgb(62,234,212),		// 95
			Color.FromArgb(64,236,211),		// 96
			Color.FromArgb(66,237,210),		// 97
			Color.FromArgb(68,238,209),		// 98
			Color.FromArgb(70,239,209),		// 99
			Color.FromArgb(72,240,208),		// 100
			Color.FromArgb(74,241,207),		// 101
			Color.FromArgb(76,242,206),		// 102
			Color.FromArgb(78,243,205),		// 103
			Color.FromArgb(80,244,204),		// 104
			Color.FromArgb(82,245,203),		// 105
			Color.FromArgb(84,246,202),		// 106
			Color.FromArgb(86,246,201),		// 107
			Color.FromArgb(88,247,200),		// 108
			Color.FromArgb(90,248,199),		// 109
			Color.FromArgb(92,249,198),		// 110
			Color.FromArgb(94,249,197),		// 111
			Color.FromArgb(96,250,196),		// 112
			Color.FromArgb(98,250,195),		// 113
			Color.FromArgb(100,251,194),		// 114
			Color.FromArgb(102,251,193),		// 115
			Color.FromArgb(104,252,192),		// 116
			Color.FromArgb(106,252,191),		// 117
			Color.FromArgb(108,253,190),		// 118
			Color.FromArgb(110,253,189),		// 119
			Color.FromArgb(112,253,188),		// 120
			Color.FromArgb(114,254,187),		// 121
			Color.FromArgb(116,254,186),		// 122
			Color.FromArgb(118,254,185),		// 123
			Color.FromArgb(120,254,184),		// 124
			Color.FromArgb(122,254,183),		// 125
			Color.FromArgb(124,254,181),		// 126
			Color.FromArgb(126,254,180),		// 127
			Color.FromArgb(128,254,179),		// 128
			Color.FromArgb(130,254,178),		// 129
			Color.FromArgb(132,254,177),		// 130
			Color.FromArgb(134,254,176),		// 131
			Color.FromArgb(136,254,175),		// 132
			Color.FromArgb(138,254,174),		// 133
			Color.FromArgb(140,254,172),		// 134
			Color.FromArgb(142,253,171),		// 135
			Color.FromArgb(144,253,170),		// 136
			Color.FromArgb(146,253,169),		// 137
			Color.FromArgb(148,252,168),		// 138
			Color.FromArgb(150,252,167),		// 139
			Color.FromArgb(152,251,165),		// 140
			Color.FromArgb(154,251,164),		// 141
			Color.FromArgb(156,250,163),		// 142
			Color.FromArgb(158,250,162),		// 143
			Color.FromArgb(160,249,161),		// 144
			Color.FromArgb(162,249,159),		// 145
			Color.FromArgb(164,248,158),		// 146
			Color.FromArgb(166,247,157),		// 147
			Color.FromArgb(168,246,156),		// 148
			Color.FromArgb(170,246,154),		// 149
			Color.FromArgb(172,245,153),		// 150
			Color.FromArgb(174,244,152),		// 151
			Color.FromArgb(176,243,151),		// 152
			Color.FromArgb(178,242,149),		// 153
			Color.FromArgb(180,241,148),		// 154
			Color.FromArgb(182,240,147),		// 155
			Color.FromArgb(184,239,146),		// 156
			Color.FromArgb(186,238,144),		// 157
			Color.FromArgb(188,237,143),		// 158
			Color.FromArgb(190,236,142),		// 159
			Color.FromArgb(192,234,140),		// 160
			Color.FromArgb(194,233,139),		// 161
			Color.FromArgb(196,232,138),		// 162
			Color.FromArgb(198,230,136),		// 163
			Color.FromArgb(200,229,135),		// 164
			Color.FromArgb(202,228,134),		// 165
			Color.FromArgb(204,226,132),		// 166
			Color.FromArgb(206,225,131),		// 167
			Color.FromArgb(208,223,130),		// 168
			Color.FromArgb(210,222,128),		// 169
			Color.FromArgb(212,220,127),		// 170
			Color.FromArgb(214,219,126),		// 171
			Color.FromArgb(216,217,124),		// 172
			Color.FromArgb(218,215,123),		// 173
			Color.FromArgb(220,214,122),		// 174
			Color.FromArgb(222,212,120),		// 175
			Color.FromArgb(224,210,119),		// 176
			Color.FromArgb(226,209,117),		// 177
			Color.FromArgb(228,207,116),		// 178
			Color.FromArgb(230,205,115),		// 179
			Color.FromArgb(232,203,113),		// 180
			Color.FromArgb(234,201,112),		// 181
			Color.FromArgb(236,199,110),		// 182
			Color.FromArgb(238,197,109),		// 183
			Color.FromArgb(240,195,108),		// 184
			Color.FromArgb(242,193,106),		// 185
			Color.FromArgb(244,191,105),		// 186
			Color.FromArgb(246,189,103),		// 187
			Color.FromArgb(248,187,102),		// 188
			Color.FromArgb(250,185,100),		// 189
			Color.FromArgb(252,183,99),		// 190
			Color.FromArgb(254,180,97),		// 191
			Color.FromArgb(255,178,96),		// 192
			Color.FromArgb(255,176,95),		// 193
			Color.FromArgb(255,174,93),		// 194
			Color.FromArgb(255,171,92),		// 195
			Color.FromArgb(255,169,90),		// 196
			Color.FromArgb(255,167,89),		// 197
			Color.FromArgb(255,164,87),		// 198
			Color.FromArgb(255,162,86),		// 199
			Color.FromArgb(255,159,84),		// 200
			Color.FromArgb(255,157,83),		// 201
			Color.FromArgb(255,154,81),		// 202
			Color.FromArgb(255,152,80),		// 203
			Color.FromArgb(255,149,78),		// 204
			Color.FromArgb(255,147,77),		// 205
			Color.FromArgb(255,144,75),		// 206
			Color.FromArgb(255,142,74),		// 207
			Color.FromArgb(255,139,72),		// 208
			Color.FromArgb(255,136,71),		// 209
			Color.FromArgb(255,134,69),		// 210
			Color.FromArgb(255,131,68),		// 211
			Color.FromArgb(255,128,66),		// 212
			Color.FromArgb(255,126,65),		// 213
			Color.FromArgb(255,123,63),		// 214
			Color.FromArgb(255,120,62),		// 215
			Color.FromArgb(255,117,60),		// 216
			Color.FromArgb(255,115,59),		// 217
			Color.FromArgb(255,112,57),		// 218
			Color.FromArgb(255,109,56),		// 219
			Color.FromArgb(255,106,54),		// 220
			Color.FromArgb(255,103,53),		// 221
			Color.FromArgb(255,100,51),		// 222
			Color.FromArgb(255,97,49),		// 223
			Color.FromArgb(255,95,48),		// 224
			Color.FromArgb(255,92,46),		// 225
			Color.FromArgb(255,89,45),		// 226
			Color.FromArgb(255,86,43),		// 227
			Color.FromArgb(255,83,42),		// 228
			Color.FromArgb(255,80,40),		// 229
			Color.FromArgb(255,77,39),		// 230
			Color.FromArgb(255,74,37),		// 231
			Color.FromArgb(255,71,36),		// 232
			Color.FromArgb(255,68,34),		// 233
			Color.FromArgb(255,65,32),		// 234
			Color.FromArgb(255,62,31),		// 235
			Color.FromArgb(255,59,29),		// 236
			Color.FromArgb(255,56,28),		// 237
			Color.FromArgb(255,53,26),		// 238
			Color.FromArgb(255,49,25),		// 239
			Color.FromArgb(255,46,23),		// 240
			Color.FromArgb(255,43,21),		// 241
			Color.FromArgb(255,40,20),		// 242
			Color.FromArgb(255,37,18),		// 243
			Color.FromArgb(255,34,17),		// 244
			Color.FromArgb(255,31,15),		// 245
			Color.FromArgb(255,28,14),		// 246
			Color.FromArgb(255,25,12),		// 247
			Color.FromArgb(255,21,10),		// 248
			Color.FromArgb(255,18,9),		// 249
			Color.FromArgb(255,15,7),		// 250
			Color.FromArgb(255,12,6),		// 251
			Color.FromArgb(255,9,4),		// 252
			Color.FromArgb(255,6,3),		// 253
			Color.FromArgb(255,3,1),		// 254
			Color.FromArgb(255,0,0),		// 255
		};
	}
}